using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string registryName;
    private string name;
    private string description;
    public Item(string registryName, string name, string description)
    {
        this.registryName = registryName;
        this.name = name;
        this.description = description;
    }

    virtual public bool OnUse(GameObject target, Transform position)
    {
        return false;
    }

    public string GetRegistryName()
    {
        return registryName;
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }
}
