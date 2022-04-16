using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour
{
    public PlayerData playerData;
    private string filePath;
    void Awake()
    {
        filePath = Application.persistentDataPath + "/player.data";
        Debug.Log("Save file at: " + Application.persistentDataPath);
        ReadData();
    }

    void OnDestroy()
    {
        WriteData();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            playerData.currentLevel = SceneManager.GetActiveScene().name;
        }

    }

    public PlayerData GetPlayerData()
    {
        return playerData;
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
            playerData.health = 10;
            playerData.playerLevel = 0;
            PlayerInventory.inventory.AddItem(ItemDatabase.ORC_WARRIOR_SPAWNER, 5);
            PlayerInventory.inventory.AddItem(ItemDatabase.DRAKE_SPAWNER, 1);
            PlayerInventory.inventory.AddItem(ItemDatabase.MASS_HEAL_ABILITY, 1);
            PlayerInventory.inventory.AddItem(ItemDatabase.HEALTH_POTION, 10);
        }
    }
    public void WriteData()
    {

        FileStream dataStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, playerData);
        dataStream.Close();
    }
}
