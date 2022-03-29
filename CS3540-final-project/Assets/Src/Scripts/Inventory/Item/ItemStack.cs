using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    private Item item;
    private int amount;
    public ItemStack(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public Item GetItem()
    {
        return item;
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
