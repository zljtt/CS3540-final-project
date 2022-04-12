using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllyBehavior : UnitBehavior
{
    protected Transform startingPoint;
    public AudioClip attackSFX;

    protected override void Awake()
    {
        base.Awake();
        // record the starting point
        //startingPoint = transform;
    }

    protected override void PerformAlert()
    {
        agent.isStopped = true;
        // try to find a target to attack
        currentAttackTarget = FindPossibleAttackTargetInRange();
        // if there is target, leave idle state
        if (currentAttackTarget != null)
        {
            currentState = State.CHASE;
        }
    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack()
    {
        // to implement in child
        agent.isStopped = true;
        if (currentAttackTarget == null)
        {
            currentState = State.ALERT;
        }
        else if (!CanReach(currentAttackTarget))
        {
            currentState = State.CHASE;
        }
        else if (lastAttackDeltaTime > attackSpeed) // attack
        {
            Attack(currentAttackTarget);
            AudioSource.PlayClipAtPoint(attackSFX, transform.position);
            lastAttackDeltaTime = 0;
        }
    }
}
