using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    private List<ItemStack> itemList;

    public Inventory()
    {
        itemList = new List<ItemStack>();
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
            return itemList[index].GetItem().OnUse(user, targetHit, itemList[index], this);
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
