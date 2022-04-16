using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : Item
{
    string unitPath;
    public SpawnerItem(string registryName, ItemProperty property, string unitPath) : base(registryName, property)
    {
        this.unitPath = unitPath;
    }

    override public bool OnUse(Transform user, RaycastHit targetHit, ItemStack stack, Inventory inventory)
    {
        if (GameObject.FindObjectOfType<CombatManager>().GetStatus() == CombatManager.STATUS.PREPARE)
        {
            GameObject spawnedUnit = GameObject.Instantiate(Resources.Load(unitPath), targetHit.point, user.parent.transform.rotation) as GameObject;
            GameObject.Instantiate(Resources.Load("Prefabs/Effects/SpawnEffect"), targetHit.point, spawnedUnit.transform.rotation);
            return true;
        }
        return false;
    }
}