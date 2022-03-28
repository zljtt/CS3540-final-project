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
                exist.SetAmount(0);
                return false;
            }
            else
            {
                exist.SetAmount(exist.GetAmount() - count);
                return false;
            }
        }
        return false;
    }

    public List<ItemStack> GetItemList()
    {
        return itemList;
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
