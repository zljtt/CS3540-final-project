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
    public static PlayerData playerData;
    public static Inventory inventory;
    public Inventory debugInventory;

    private string playerDataSavePath;
    private string inventorySavePath;
    void Awake()
    {
        playerDataSavePath = Application.persistentDataPath + "/player.data";
        inventorySavePath = Application.persistentDataPath + "/inventory.data";
        Debug.Log("Save file at: " + Application.persistentDataPath);
        ReadPlayerData();
        ReadInventoryData();
    }

    void Update()
    {
        debugInventory = inventory;
        // update cooldown
        foreach (ItemStack itemStack in inventory.GetItemList())
        {
            if (itemStack != Inventory.EMPTY)
            {
                itemStack.UpdateCooldown(Time.deltaTime);
            }
        }
    }

    void OnDestroy()
    {
        WritePlayerData();
        WriteInventoryData();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            playerData.currentLevel = SceneManager.GetActiveScene().name;
        }

    }
    public void ReadPlayerData()
    {
        playerData = new PlayerData();
        if (File.Exists(playerDataSavePath))
        {
            FileStream dataStream = new FileStream(playerDataSavePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            try
            {
                playerData = converter.Deserialize(dataStream) as PlayerData;
                dataStream.Close();
            }
            catch (Exception)
            {
                dataStream.Close();
                File.Delete(playerDataSavePath);
            }
        }
    }
    public void WritePlayerData()
    {
        FileStream dataStream = new FileStream(playerDataSavePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        try
        {
            converter.Serialize(dataStream, playerData);
            dataStream.Close();
        }
        catch (Exception)
        {
            dataStream.Close();
            File.Delete(playerDataSavePath);
        }
    }

    public void ReadInventoryData()
    {
        inventory = new Inventory();
        if (File.Exists(inventorySavePath))
        {
            FileStream dataStream = new FileStream(inventorySavePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            try
            {
                inventory = converter.Deserialize(dataStream) as Inventory;
                dataStream.Close();
            }
            catch (Exception)
            {
                dataStream.Close();
                File.Delete(inventorySavePath);
            }
        }
    }
    public void WriteInventoryData()
    {
        FileStream dataStream = new FileStream(inventorySavePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        try
        {
            converter.Serialize(dataStream, inventory);
            dataStream.Close();
        }
        catch (Exception)
        {
            dataStream.Close();
            File.Delete(inventorySavePath);
        }

    }
}
