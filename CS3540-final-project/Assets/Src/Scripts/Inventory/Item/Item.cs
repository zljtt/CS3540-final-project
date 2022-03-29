using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string registryName;
    private string displayName;
    private string description;
    public Item(string registryName, string displayName, string description)
    {
        this.registryName = registryName;
        this.displayName = displayName;
        this.description = description;
    }

    virtual public bool OnUse(Transform user, RaycastHit targetHit, ItemStack stack, Inventory inventory)
    {
        return false;
    }

    public string GetRegistryName()
    {
        return registryName;
    }

    public string GetName()
    {
        return displayName;
    }

    public string GetDescription()
    {
        return description;
    }

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Sprites/Items/" + registryName);
    }
}
