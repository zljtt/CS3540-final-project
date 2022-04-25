using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class StoreManager : MonoBehaviour
{

    public static Store store;
    private string storeSavePath;
    public static bool nextLoadRefill;
    void Awake()
    {
        storeSavePath = Application.persistentDataPath + "/store.data";
        if (nextLoadRefill)
        {
            nextLoadRefill = false;
            store = new Store();
            store.Refill();
        }
        else
        {
            ReadStoreData();
        }
    }
    void OnDestroy()
    {
        WriteStoreData();
    }

    public void ReadStoreData()
    {
        store = new Store();
        if (File.Exists(storeSavePath))
        {
            FileStream dataStream = new FileStream(storeSavePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            try
            {
                store = converter.Deserialize(dataStream) as Store;
                dataStream.Close();
            }
            catch (Exception)
            {
                dataStream.Close();
                File.Delete(storeSavePath);
            }
        }
    }
    public void WriteStoreData()
    {
        FileStream dataStream = new FileStream(storeSavePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        try
        {
            converter.Serialize(dataStream, store);
            dataStream.Close();
        }
        catch (Exception)
        {
            dataStream.Close();
            File.Delete(storeSavePath);
        }
    }
}
