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

    override public bool OnUse(Transform user, RaycastHit targetHit, int index)
    {
        if (GameObject.FindObjectOfType<CombatManager>().GetStatus() == CombatManager.STATUS.PREPARE)
        {
            GameObject spawnedUnit = GameObject.Instantiate(Resources.Load(unitPath), targetHit.point, user.parent.transform.rotation) as GameObject;
            GameObject.Instantiate(Resources.Load("Prefabs/Effects/SpawnEffect"), targetHit.point, spawnedUnit.transform.rotation);
            ApplyRitual(spawnedUnit.GetComponent<UnitBehavior>());
            LevelManager.inventory.Consume(index, 1);
            return true;
        }
        return false;
    }

    private void ApplyRitual(UnitBehavior unit)
    {
        unit.maxHealth = (int)(unit.maxHealth * (1 + LevelManager.inventory.CountItemInHotbar(ItemDatabase.HEALTH_RITUAL) * 0.2f));
    }
}