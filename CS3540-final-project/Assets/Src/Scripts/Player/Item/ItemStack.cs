using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemStack
{
    public int item;
    public int amount;
    private float currentCooldown;
    public ItemStack(Item item, int amount)
    {
        this.item = item.GetID();
        this.amount = amount;
        currentCooldown = 0;
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

    public float GetMaxCooldown()
    {
        return this.GetItem().GetProperties().GetMaxCooldown();
    }

    public float GetCurrentCooldown()
    {
        return this.currentCooldown;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (currentCooldown > 0)
        {
            this.currentCooldown -= deltaTime;
        }
    }

    public bool Use(Transform user, RaycastHit targetHit, Inventory inventory)
    {
        if (currentCooldown <= 0)
        {
            if (this.GetItem().OnUse(user, targetHit, this, inventory))
            {
                currentCooldown = this.GetItem().GetProperties().GetMaxCooldown();
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
