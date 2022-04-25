using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    protected int healAmount;
    public HealingItem(string registryName, ItemProperty property, int healAmount) : base(registryName, property)
    {
        this.healAmount = healAmount;
    }

    override public bool OnUse(Transform user, RaycastHit targetHit, int index)
    {
        var target = targetHit.collider.GetComponent<AllyBehavior>();
        if (target != null)
        {
            target.ApplyEffect(EffectType.HEALING, healAmount / 5);
            LevelManager.inventory.Consume(index, 1);
            return true;
        }
        return false;
    }
}
