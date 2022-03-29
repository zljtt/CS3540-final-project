using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : Item
{
    string unitPath;
    public SpawnerItem(string registryName, string name, string description, string unitPath) : base(registryName, name, description)
    {
        this.unitPath = unitPath;
    }

    override public bool OnUse(Transform user, RaycastHit targetHit, ItemStack stack, Inventory inventory)
    {
        GameObject.Instantiate(Resources.Load(unitPath), targetHit.point, user.rotation);
        inventory.RemoveItem(stack, 1);
        return true;
    }
}