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
