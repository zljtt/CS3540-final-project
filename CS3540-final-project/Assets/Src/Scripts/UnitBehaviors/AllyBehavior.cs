using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllyBehavior : UnitBehavior
{
    protected override void Update()
    {
        base.Update();

        GameObject target = FindPossibleTarget();
        if (target != null)
        {
            if (CanReach(target))
            {
                Attack(target);
            }
            else
            {
                MoveTowardTarget(target);
            }
        }
        else
        {
            // idle
        }
    }
}
