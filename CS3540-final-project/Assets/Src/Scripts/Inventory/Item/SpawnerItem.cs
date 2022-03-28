using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : Item
{
    GameObject unit;
    public SpawnerItem(string registryName, string name, string description, GameObject unit) : base(registryName, name, description)
    {
        this.unit = unit;
    }

    override public bool OnUse(GameObject target, Transform position)
    {
        Instan
        return true;
    }
}