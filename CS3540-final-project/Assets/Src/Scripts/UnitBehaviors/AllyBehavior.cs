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
            FaceTarget(currentAttackTarget.transform.position);
        }
    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack()
    {
        agent.isStopped = true;
        int picker = Random.Range(0, 1);
        if(picker == 0) {
            anim.SetInteger("animState", 3);               
        }
        else {
            anim.SetInteger("animState", 4);
        }

        if (currentAttackTarget == null)
        {
            currentState = State.ALERT;
        }
        else if (!CanReach(currentAttackTarget))
        {
            FaceTarget(currentAttackTarget.transform.position);
            currentState = State.CHASE;
        }
        else if (lastAttackDeltaTime > attackSpeed) // attack
        {
            Attack(currentAttackTarget);
            FaceTarget(currentAttackTarget.transform.position);
            AudioSource.PlayClipAtPoint(attackSFX, transform.position);
            lastAttackDeltaTime = 0;
        }
    }
}
