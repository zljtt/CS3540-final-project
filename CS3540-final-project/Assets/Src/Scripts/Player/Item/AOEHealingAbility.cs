using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHealingAbility : HealingItem
{
    protected float radius;
    public AOEHealingAbility(string registryName, ItemProperty property, int healAmount, float radius) : base(registryName, property, healAmount)
    {
        this.radius = radius;
    }

    override public bool OnUse(Transform user, RaycastHit targetHit, int index)
    {
        Collider[] colliders = Physics.OverlapSphere(targetHit.point, radius);
        GameObject abilityVFX = GameObject.Instantiate(Resources.Load("Prefabs/Effects/Healing_Nature"), targetHit.point, Quaternion.Euler(0, 0, 0)) as GameObject;
        foreach (Collider c in colliders)
        {
            var target = c.GetComponent<AllyBehavior>();
            if (target != null)
            {
                target.ApplyEffect(EffectType.HEALING, healAmount / 5);
            }
        }
        return true;
    }
}
