using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory
{
    public static readonly ItemStack EMPTY = new ItemStack(ItemDatabase.EMPTY, 0);
    public static readonly int SIZE = 36;
    public ItemStack[] items;

    public Inventory()
    {
        items = new ItemStack[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            items[i] = EMPTY;
        }
        items[0] = new ItemStack(ItemDatabase.ORC_WARRIOR_SPAWNER, 10);
        items[1] = new ItemStack(ItemDatabase.DRAKE_SPAWNER, 10);
        items[2] = new ItemStack(ItemDatabase.HEALTH_POTION, 3);
        items[23] = new ItemStack(ItemDatabase.MASS_HEAL_ABILITY, 1);
    }

    public ItemStack AddItem(Item item, int count)
    {
        int amountLeft = count; // Items left that need to be added
        List<int> loStack = GetItemStackIndexes(item);
        // For each stack in the list...
        foreach (int stackIndex in loStack)
        {
            // Check to see if the stack's amount is less than max stack
            if (items[stackIndex].GetAmount() < item.GetProperties().GetMaxStack())
            {
                // if so, determine the amount of items that can be added to this stack
                // which is the lesser of the amount left to add, or the max amount of item this stack can take
                int amountToAdd = Mathf.Min(amountLeft, item.GetProperties().GetMaxStack() - items[stackIndex].GetAmount());
                items[stackIndex].SetAmount(items[stackIndex].GetAmount() + amountToAdd);
                amountLeft -= amountToAdd;
                if (amountLeft == 0)
                {
                    return EMPTY;
                }
            }
        }
        // After going through the list of stack, if there's still items left, create a while loop to add new stacks
        while (amountLeft > 0)
        {
            int amountToAdd = Mathf.Min(amountLeft, item.GetProperties().GetMaxStack());
            int index;
            if ((index = NextEmpty()) == -1)
            {
                return new ItemStack(item, amountLeft);
            }
            items[index] = new ItemStack(item, amountToAdd);
            amountLeft -= amountToAdd;
        }
        return EMPTY;
    }

    public bool RemoveItem(Item item, int count)
    {
        int amountLeft = count; // Items left that need to be removed
        List<int> loStack = GetItemStackIndexes(item);
        // First, determine if there's enough item to remove
        int totalItem = 0;
        foreach (int stackIndex in loStack)
        {
            totalItem += items[stackIndex].GetAmount();
        }
        if (totalItem < amountLeft)
        {
            return false;
        }
        // For each stack in the list, starting from the back...
        for (int back = loStack.Count - 1; back >= 0; back--)
        {
            Debug.Log("Removing from a stack");
            ItemStack stack = items[loStack[back]];
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
                    items[loStack[back]] = EMPTY;
                }
                if (amountLeft == 0)
                {
                    return true;
                }
            }
        }
        return true;
    }

    public bool Consume(int index, int count)
    {
        if (index < 0 || index >= items.Length || count > items[index].GetAmount())
        {
            return false;
        }
        else
        {
            items[index].SetAmount(items[index].GetAmount() - count);
            if (items[index].GetAmount() <= 0)
            {
                items[index] = EMPTY;
            }
            return true;
        }
    }

    public List<ItemStack> GetItemList()
    {
        return new List<ItemStack>(items);
    }

    public ItemStack GetItemAt(int index)
    {
        return items[index];
    }

    public int NextEmpty()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == EMPTY)
            {
                return i;
            }
        }
        return -1;
    }
    public void Swap(int index1, int index2)
    {
        ItemStack temp = items[index1];
        items[index1] = items[index2];
        items[index2] = temp;
    }

    private List<int> GetItemStackIndexes(Item item)
    {
        List<int> loStack = new List<int>();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != EMPTY && items[i].GetItem().Equals(item))
            {
                loStack.Add(i);
            }
        }
        return loStack;
    }
}
