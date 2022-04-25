using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSkill : Item
{
    public TauntSkill(string registryName, ItemProperty property) : base(registryName, property)
    {
    }

    override public bool OnUse(Transform user, RaycastHit targetHit, int index)
    {
        var target = targetHit.collider.GetComponent<AllyBehavior>();
        if (target != null)
        {
            List<GameObject> targets = target.FindTargetsInRange(new string[] { "Enemy" });
            foreach (GameObject enemy in targets)
            {
                enemy.GetComponent<UnitBehavior>().currentAttackTarget = targetHit.collider.gameObject;
            }
            return true;
        }
        return false;
    }
}