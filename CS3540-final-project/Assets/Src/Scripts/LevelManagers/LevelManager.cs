using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelManager : MonoBehaviour
{
    public enum STATUS { PREPARE, COMBAT, LOOT };
    public float prepareTime = 30;
    public float combatTime = 60;
    public Text statusText;
    public Text healthText;

    public Slider timeSlider;
    public int playerHealth = 10;

    protected float currentTime;
    protected STATUS status;
    protected List<SpawnInfo> spawnList;
    void Start()
    {
        status = STATUS.PREPARE;
        currentTime = 0;
        spawnList = GetSpawns();
        OnPrepareStart();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        statusText.text = status.ToString();
        healthText.text = ("HP LEFT : " + playerHealth.ToString());
        switch (status)
        {
            case STATUS.PREPARE:
                timeSlider.maxValue = prepareTime;
                timeSlider.value = currentTime;
                // move to next stage
                if (currentTime >= prepareTime)
                {
                    status = STATUS.COMBAT;
                    OnCombatStart();
                }
                break;
            case STATUS.COMBAT:
                timeSlider.maxValue = combatTime;
                timeSlider.value = currentTime - prepareTime;
                // spawn in game
                spawnList.RemoveAll(spawn => spawn.CheckSpawn(currentTime - prepareTime));
                // move to next stage
                if (currentTime >= prepareTime + combatTime)
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
    public void LoseHealth(int amount)
    {
        if (playerHealth > 0)
        {
            playerHealth -= amount;
        }
    }

    public STATUS GetStatus()
    {
        return status;
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

        public bool CheckSpawn(float currentTime)
        {
            if (currentTime > time)
            {
                GameObject spawnedUnit = GameObject.Instantiate(Resources.Load(unit), position, rotation) as GameObject;
                UnitBehavior behavior = spawnedUnit.GetComponent<UnitBehavior>();
                if (behavior != null)
                {
                    behavior.ChangeState(UnitBehavior.State.ALERT);
                }
                return true;
            }
            return false;
        }
    }

}
