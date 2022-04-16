using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemStack
{
    public int item;
    public int amount;
    private float useTime;
    public ItemStack(Item item, int amount)
    {
        this.item = item.GetID();
        this.amount = amount;
        useTime = 0;
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
    public void UpdateCooldown(float deltaTime)
    {
        this.useTime += deltaTime;
    }

    public bool Use(Transform user, RaycastHit targetHit, Inventory inventory)
    {
        if (useTime > this.GetItem().GetProperties().getUseCoolDown())
        {
            useTime = 0;
            return this.GetItem().OnUse(user, targetHit, this, inventory);
        }
        return false;
    }
}
