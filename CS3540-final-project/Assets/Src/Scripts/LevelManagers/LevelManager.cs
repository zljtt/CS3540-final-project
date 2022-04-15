using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public abstract class LevelManager : MonoBehaviour
{
    public enum STATUS { PREPARE, COMBAT, LOOT };
    public float prepareTime = 30;
    public float combatTime = 60;
    public Text statusText;
    public Text healthText;
    public List<Transform> spawnPoints;
    public Slider timeSlider;
    public PlayerData playerData;

    protected float currentTime;
    protected float pathIndicaterTime;

    protected STATUS status;
    protected List<SpawnInfo> spawnList;
    protected List<GameObject> spawnedEnemyList;
    //protected int maxEnemyCount;
    private string filePath;
    void Awake()
    {
        filePath = Application.persistentDataPath + "/player.data";
        ReadData();
        if (playerData.currentLevel != SceneManager.GetActiveScene().name)
        {
            if (playerData.currentLevel == null || playerData.currentLevel == "")
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                SceneManager.LoadScene(playerData.currentLevel);
            }
        }
    }

    void OnDestroy()
    {
        WriteData();
    }

    void Start()
    {
        status = STATUS.PREPARE;
        currentTime = 0;
        pathIndicaterTime = 0;
        spawnList = GetSpawns();
        spawnedEnemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        //maxEnemyCount = spawnList.Count + spawnedEnemyList.Count;
        OnPrepareStart();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        pathIndicaterTime += Time.deltaTime;
        statusText.text = status.ToString();
        healthText.text = ("HP LEFT : " + playerData.health.ToString());
        switch (status)
        {
            case STATUS.PREPARE:
                // spawn enemy path indicater
                if (pathIndicaterTime > 3)
                {
                    foreach (var spawn in spawnPoints)
                    {
                        GameObject pathIndicater = GameObject.Instantiate(Resources.Load("Prefabs/Others/PathIndicater"), spawn.position, spawn.rotation) as GameObject;
                    }
                    pathIndicaterTime = 0;
                }
                timeSlider.maxValue = prepareTime;
                timeSlider.value = currentTime;
                // move to next stage
                if (Input.GetKey(KeyCode.P))
                {
                    currentTime = prepareTime;
                    status = STATUS.COMBAT;
                    OnCombatStart();
                }
                if (currentTime >= prepareTime)
                {
                    status = STATUS.COMBAT;
                    OnCombatStart();
                }
                break;
            case STATUS.COMBAT:
                timeSlider.maxValue = spawnList.Count + spawnedEnemyList.Count;
                int killedEnemyCount = 0;
                spawnedEnemyList.ForEach(enemy => { if (enemy == null) killedEnemyCount++; });
                timeSlider.value = killedEnemyCount;
                // spawn in game
                spawnList.RemoveAll(spawn => spawn.CheckSpawn(currentTime - prepareTime, spawnedEnemyList));
                // spawnedEnemyList.RemoveAll(enemy => enemy == null);
                // move to next stage
                //if (spawnList >= prepareTime + combatTime)
                if (timeSlider.value == timeSlider.maxValue)
                {
                    status = STATUS.LOOT;
                    OnCombatEnd();
                }
                break;
            case STATUS.LOOT:
                timeSlider.enabled = false;
                // implement later
                break;
            default:
                break;
        }
    }
    protected abstract void OnPrepareStart();
    protected abstract void OnCombatStart();
    protected abstract void OnCombatEnd();

    protected abstract List<SpawnInfo> GetSpawns();

    public STATUS GetStatus()
    {
        return status;
    }

    public void ReadData()
    {
        if (File.Exists(filePath))
        {
            FileStream dataStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            playerData = converter.Deserialize(dataStream) as PlayerData;
            dataStream.Close();
        }
        else
        {
            playerData = new PlayerData();
        }
    }
    public void WriteData()
    {

        FileStream dataStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, playerData);
        dataStream.Close();
    }

    public class SpawnInfo
    {
        int time;
        Vector3 position;
        Quaternion rotation;
        string unit;
        public SpawnInfo(int t, string u, Vector3 p, Quaternion r)
        {
            time = t;
            position = p;
            rotation = r;
            unit = u;
        }

        public bool CheckSpawn(float currentTime, List<GameObject> spawned)
        {
            if (currentTime > time)
            {
                GameObject spawnedUnit = GameObject.Instantiate(Resources.Load(unit), position, rotation) as GameObject;
                UnitBehavior behavior = spawnedUnit.GetComponent<UnitBehavior>();
                if (behavior != null)
                {
                    behavior.ChangeState(UnitBehavior.State.ALERT);
                }
                spawned.Add(spawnedUnit);
                return true;
            }
            return false;
        }
    }

}
