using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StoreEntry
{
    public ItemStack itemStack;
    public int value;

    public StoreEntry(ItemStack item, int v)
    {
        itemStack = item;
        value = v;
    }

    public override bool Equals(object obj)
    {
        return obj is StoreEntry && itemStack == ((StoreEntry)obj).itemStack && value == ((StoreEntry)obj).value;
    }

    public override int GetHashCode()
    {
        return itemStack.GetHashCode() + value.GetHashCode();
    }

    public static bool operator ==(StoreEntry a, StoreEntry b)
    {
        return a.itemStack == b.itemStack && a.value == b.value;
    }

    public static bool operator !=(StoreEntry a, StoreEntry b)
    {
        return a.itemStack != b.itemStack || a.value != b.value;
    }
}

[Serializable]
public class Store
{
    public static readonly StoreEntry EMPTY_ENTRY = new StoreEntry(Inventory.EMPTY, -1);
    public static readonly int SIZE = 8;
    public StoreEntry[] entries;

    public Store()
    {
        entries = new StoreEntry[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            entries[i] = EMPTY_ENTRY;
        }
        Refill();
    }

    public bool Purchase(int index)
    {
        bool buySuccess = LevelManager.playerData.ConsumeMoney(entries[index].value);
        if (buySuccess)
        {
            LevelManager.inventory.AddItem(entries[index].itemStack.GetItem(), entries[index].itemStack.GetAmount());
            entries[index] = EMPTY_ENTRY;
        }
        return buySuccess;
    }

    public void Refill()
    {
        for (int i = 0; i < SIZE; i++)
        {
            entries[i] = StoreEntries.ENTRIES[UnityEngine.Random.Range(0, StoreEntries.ENTRIES.Count)];
        }
    }

    public StoreEntry GetEntryAt(int index)
    {
        return entries[index];
    }
}
