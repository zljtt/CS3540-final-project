using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : UnitBehavior
{
    private GameObject wayPoint;
    protected override void Start()
    {
        base.Start();
        // init a list of waypoints at first, and remove the ones it reaches later
        List<GameObject> wayPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));
        wayPoint = FindClosest(transform, wayPoints);
    }

    protected override void Update()
    {
        agent.speed = currentState == State.CHASE ? moveSpeed : moveSpeed * 1.5f;
        Vector3 steer = agent.steeringTarget - transform.position;

        agent.isStopped = currentState == State.IDLE || currentState == State.DIE
            || currentState == State.ATTACK  // if steering target is incorrect during combat
                && Vector2.Angle(new Vector2(steer.x, steer.z), new Vector2(transform.forward.x, transform.forward.z)) < 10f;

        base.Update();
    }

    protected override void PerformAlert()
    {
        anim.SetInteger("animState", WALK_ANIM);
        currentAttackTarget = FindPossibleAttackTargetInRange();
        if (currentAttackTarget == null)
        {
            // if there is no target to attack, go to the closest waypoint
            agent.SetDestination(wayPoint.transform.position);
            if (Vector3.Distance(transform.position, wayPoint.transform.position) < 2)
            {
                // player lose health
                LevelManager.playerData.LoseHealth(1);
                Destroy(gameObject);
            }
        }
        else
        {
            //FaceTarget(currentAttackTarget.transform.position);
            currentState = State.CHASE;
        }
    }

    // when an unit has a target, it will chase the target until entering attack range
    protected override void PerformChase()
    {
        anim.SetInteger("animState", RUN_ANIM);
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
            lastAttackDeltaTime = 0;
        }
    }

    protected override void PerformDie()
    {
        anim.SetInteger("animState", DIE_ANIM);
        Destroy(gameObject, 2);
    }
}
