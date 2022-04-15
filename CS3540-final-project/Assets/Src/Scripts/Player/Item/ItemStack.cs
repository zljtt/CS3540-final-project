using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemStack
{
    public int item;
    public int amount;
    public ItemStack(Item item, int amount)
    {
        this.item = item.GetID();
        this.amount = amount;
    }

    public Item GetItem()
    {
        return ItemDatabase.ITEMS[item];
    }

    public void SetAmount(int amount)
    {
        this.amount = Mathf.Max(0, amount);
    }

    public int GetAmount()
    {
        return this.amount;
    }
}
