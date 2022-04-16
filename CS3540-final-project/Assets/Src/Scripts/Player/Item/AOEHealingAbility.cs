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

    override public bool OnUse(Transform user, RaycastHit targetHit, ItemStack stack, Inventory inventory)
    {
        Collider[] colliders = Physics.OverlapSphere(targetHit.point, radius);
        GameObject abilityVFX = GameObject.Instantiate(Resources.Load("Prefabs/Effects/SphereEffect"), targetHit.point, Quaternion.Euler(0, 0, 0)) as GameObject;
        abilityVFX.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        abilityVFX.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.5f); // Change to a green-ish color
        foreach (Collider c in colliders)
        {
            var target = c.GetComponent<AllyBehavior>();
            if (target != null)
            {
                target.Heal(healAmount);
                GameObject effect = GameObject.Instantiate(Resources.Load("Prefabs/Effects/HealEffect"), target.transform.position, Quaternion.Euler(-90, 0, 90)) as GameObject;
                effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                effect.transform.parent = target.transform;
            }
        }
        return true;
    }
}