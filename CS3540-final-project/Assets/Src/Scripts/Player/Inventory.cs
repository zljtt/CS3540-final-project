using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Inventory
{
    private List<ItemStack> itemList;
    private string filePath = Application.persistentDataPath + "/inventory.data";

    public Inventory()
    {
        //itemList = new List<ItemStack>();
    }

    public void ReadData()
    {
        if (File.Exists(filePath))
        {
            FileStream dataStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            itemList = converter.Deserialize(dataStream) as List<ItemStack>;
            dataStream.Close();
        }
        else
        {
            itemList = new List<ItemStack>();

            AddItem(ItemDatabase.ORC_WARRIOR_SPAWNER, 5);
            AddItem(ItemDatabase.DRAKE_SPAWNER, 1);
            AddItem(ItemDatabase.MASS_HEAL_ABILITY, 1);
            AddItem(ItemDatabase.HEALTH_POTION, 10);
        }
    }
    public void WriteData()
    {
        FileStream dataStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, itemList);
        dataStream.Close();
    }

    public bool AddItem(Item item, int count)
    {
        ItemStack exist;
        if ((exist = GetItemStack(item)) != null)
        {
            exist.SetAmount(exist.GetAmount() + count);
        }
        else
        {
            itemList.Add(new ItemStack(item, count));
        }
        return true;
    }

    public bool RemoveItem(Item item, int count)
    {
        ItemStack exist;
        if ((exist = GetItemStack(item)) != null)
        {
            if (count > exist.GetAmount())
            {
                // exist.SetAmount(0);
                return false;
            }
            else
            {
                exist.SetAmount(exist.GetAmount() - count);
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(ItemStack stack, int count)
    {
        if (count > stack.GetAmount())
        {
            // exist.SetAmount(0);
            return false;
        }
        else
        {
            stack.SetAmount(stack.GetAmount() - count);
            if (stack.GetAmount() <= 0)
            {
                itemList.Remove(stack);
            }
            return true;
        }
    }

    public List<ItemStack> GetItemList()
    {
        return itemList;
    }

    public bool UseItemAtIndex(int index, Transform user, RaycastHit targetHit)
    {
        if (itemList.Count > index)
        {
            return itemList[index].Use(user, targetHit, this);
        }
        return false;
    }

    private ItemStack GetItemStack(Item item)
    {
        foreach (ItemStack i in itemList)
        {
            if (i.GetItem().Equals(item))
            {
                return i;
            }
        }
        return null;
    }
}
