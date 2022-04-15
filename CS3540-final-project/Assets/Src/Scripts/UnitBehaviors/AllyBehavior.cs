using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllyBehavior : UnitBehavior
{
    protected Transform startingPoint;
    public AudioClip attackSFX;

    protected override void Start()
    {
        base.Start();
        // record the starting point
        //startingPoint = transform;
    }

    protected override void Update()
    {
        agent.speed = currentState == State.CHASE ? moveSpeed : moveSpeed * 1.5f;
        agent.isStopped = currentState == State.ALERT || currentState == State.DIE || currentState == State.IDLE;
        // if steering target is incorrect during combat
        Vector3 steer = agent.steeringTarget - transform.position;
        agent.isStopped = currentState == State.ATTACK && Vector2.Angle(new Vector2(steer.x, steer.z), new Vector2(transform.forward.x, transform.forward.z)) < 10f;
        base.Update();
    }

    protected override void PerformAlert()
    {
        anim.SetInteger("animState", IDLE_ANIM);
        // try to find a target to attack
        currentAttackTarget = FindPossibleAttackTargetInRange();
        // if there is target, leave idle state
        if (currentAttackTarget != null)
        {
            currentState = State.CHASE;
            //FaceTarget(currentAttackTarget.transform.position);
        }
    }

    protected override void PerformChase()
    {
        anim.SetInteger("animState", WALK_ANIM);
        if (currentAttackTarget == null)
        {
            currentState = State.ALERT;
        }
        else if (CanReach(currentAttackTarget))
        {
            currentState = State.ATTACK;
        }
        else // chase
        {
            agent.SetDestination(currentAttackTarget.transform.position);
        }
    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack()
    {
        if (currentAttackTarget == null || !CanReach(currentAttackTarget))
        {
            currentState = State.ALERT;
        }
        else if (lastAttackDeltaTime > attackSpeed) // attack
        {
            agent.SetDestination(currentAttackTarget.transform.position); // keep rotation
            Attack(currentAttackTarget);
            AudioSource.PlayClipAtPoint(attackSFX, transform.position);
            lastAttackDeltaTime = 0;
        }
    }

    protected override void PerformDie()
    {
        anim.SetInteger("animState", DIE_ANIM);
        Destroy(gameObject, 2);
    }
}
