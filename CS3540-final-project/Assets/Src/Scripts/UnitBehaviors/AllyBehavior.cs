using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllyBehavior : UnitBehavior
{
    protected Transform startingPoint;
    protected override void Awake()
    {
        base.Awake();
        // record the starting point
        //startingPoint = transform;
    }
    protected override void Update()
    {
        base.Update();
        if (!active) return;
        // try to find a target to attack if there is no previous or the previous is dead
        if (currentTarget == null)
        {
            currentTarget = FindPossibleAttackTarget();
        }
        // if there is a target to attack, try to attack
        if (currentTarget != null)
        {
            if (CanReach(currentTarget))
            {
                if (lastAttackDeltaTime > attackSpeed)
                {
                    Attack(currentTarget);
                    lastAttackDeltaTime = 0;
                }
            }
            else
            {
                MoveTowardTarget(currentTarget.transform);
            }
        }
        // if there is no target to attack, go back to the starting point
        else
        {
            //MoveTowardTarget(startingPoint);
        }
    }
}
