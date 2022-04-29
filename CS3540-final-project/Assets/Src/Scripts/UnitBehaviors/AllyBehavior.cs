using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllyBehavior : UnitBehavior
{
    protected Transform startingPoint;


    protected override void Start()
    {
        base.Start();
        // record the starting point
        //startingPoint = transform;
    }

    protected override void Update()
    {
        if (agent != null && agent.enabled)
        {
            agent.stoppingDistance = 0.2f;
            agent.speed = currentState == State.CHASE ? GetMoveSpeed() : GetMoveSpeed() * 1.5f;
            agent.isStopped = currentState == State.ALERT || currentState == State.DIE || currentState == State.IDLE || currentState == State.ATTACK;
        }
        base.Update();
    }

    protected override void UpdateEffectPerSecond()
    {
        base.UpdateEffectPerSecond();
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in allies)
        {
            if (Vector3.Distance(ally.transform.position, transform.position) < 5)
            {
                modifiedAttackDamage *= (1 + LevelManager.inventory.CountItemInHotbar(ItemDatabase.WOLF_RITUAL) * 0.25f);
            }
        }
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
        else if (CanReach(currentAttackTarget) && TargetInSight())
        {
            currentState = State.ATTACK;
        }
        else if (agent != null && agent.enabled)// chase
        {
            agent.SetDestination(currentAttackTarget.transform.position);
        }
    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack()
    {
        anim.SetInteger("animState", ATTACK_ANIM);
        if (currentAttackTarget == null || !CanReach(currentAttackTarget))
        {
            currentState = State.ALERT;
        }
        else if (!TargetInSight())
        {
            currentState = State.CHASE;
        }
        else if (lastAttackDeltaTime > GetAttackSpeed()) // attack
        {
            Attack(currentAttackTarget);
            lastAttackDeltaTime = 0;
        }
    }

    protected override void PerformDie()
    {
        anim.SetInteger("animState", DIE_ANIM);
        Destroy(gameObject, 2);
    }
}
