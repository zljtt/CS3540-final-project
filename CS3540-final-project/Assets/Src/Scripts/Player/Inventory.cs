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
        int amountLeft = count; // Items left that need to be added
        List<ItemStack> loStack = GetItemStacks(item);
        // For each stack in the list...
        foreach (ItemStack stack in loStack)
        {
            // Check to see if the stack's amount is less than max stack
            if (stack.GetAmount() < item.GetProperties().GetMaxStack())
            {
                // if so, determine the amount of items that can be added to this stack
                // which is the lesser of the amount left to add, or the max amount of item this stack can take
                int amountToAdd = Mathf.Min(amountLeft, item.GetProperties().GetMaxStack() - stack.GetAmount());
                stack.SetAmount(stack.GetAmount() + amountToAdd);
                amountLeft -= amountToAdd;
                if (amountLeft == 0)
                {
                    return true;
                }
            }
        }
        // After going through the list of stack, if there's still items left, create a while loop to add new stacks
        while (amountLeft > 0)
        {
            int amountToAdd = Mathf.Min(amountLeft, item.GetProperties().GetMaxStack());
            itemList.Add(new ItemStack(item, amountToAdd));
            amountLeft -= amountToAdd;
        }
        return true;
    }

    public bool RemoveItem(Item item, int count)
    {
        int amountLeft = count; // Items left that need to be removed
        List<ItemStack> loStack = GetItemStacks(item);
        // First, determine if there's enough item to remove
        int totalItem = 0;
        foreach (ItemStack stack in loStack)
        {
            totalItem += stack.GetAmount();
        }
        if (totalItem < amountLeft)
        {
            return false;
        }
        // For each stack in the list, starting from the back...
        for (int back = loStack.Count - 1; back > 0; back--)
        {
            ItemStack stack = loStack[back];
            // Check to see if the stack's amount is less than max stack
            if (stack.GetAmount() > 0)
            {
                // determine the amount of items that can be removed from this stack
                // which is the lesser of the amount left to remove, or the max amount of item in this stack
                int amountToRemove = Mathf.Min(amountLeft, stack.GetAmount());
                stack.SetAmount(stack.GetAmount() - amountToRemove);
                amountLeft -= amountToRemove;
                // remove this stack if all item in this stack is removed
                if (stack.GetAmount() == 0)
                {
                    itemList.Remove(stack);
                }
                if (amountLeft == 0)
                {
                    return true;
                }
            }
        }
        return true;
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

    private List<ItemStack> GetItemStacks(Item item)
    {
        List<ItemStack> loStack = new List<ItemStack>();
        foreach (ItemStack i in itemList)
        {
            if (i.GetItem().Equals(item))
            {
                loStack.Add(i);
            }
        }
        return loStack;
    }
}
