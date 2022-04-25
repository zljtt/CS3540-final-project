using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAllyTargetItem : Item
{
    protected int duration;
    protected bool consume;
    protected EffectType effect;
    public SingleAllyTargetItem(string registryName, ItemProperty property, EffectType effect, int duration, bool consume) : base(registryName, property)
    {
        this.duration = duration;
        this.effect = effect;
        this.consume = consume;
    }

    override public bool OnUse(Transform user, RaycastHit targetHit, int index)
    {
        var target = targetHit.collider.GetComponent<AllyBehavior>();
        if (target != null)
        {
            target.ApplyEffect(effect, duration);
            if (consume)
            {
                LevelManager.inventory.Consume(index, 1);
            }
            return true;
        }
        return false;
    }
}