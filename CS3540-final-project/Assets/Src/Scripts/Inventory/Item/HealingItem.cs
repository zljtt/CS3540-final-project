using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    int healAmount;
    public HealingItem(string registryName, string name, string description, int healAmount) : base(registryName, name, description)
    {
        this.healAmount = healAmount;
    }

    override public bool OnUse(Transform user, RaycastHit targetHit, ItemStack stack, Inventory inventory)
    {
        var target = targetHit.transform.parent.GetComponent<AllyBehavior>();
        if (target)
        {
            target.TakeHealth(healAmount);
            GameObject effect = GameObject.Instantiate(Resources.Load("Prefabs/Effects/HealEffect"), target.transform.position, Quaternion.Euler(-90, 0, 90)) as GameObject;
            effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            effect.transform.parent = target.transform;
            inventory.RemoveItem(stack, 1);
            return true;
        }
        return false;
    }
}
