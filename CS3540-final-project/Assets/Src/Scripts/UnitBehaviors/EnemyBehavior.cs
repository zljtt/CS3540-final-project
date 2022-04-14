using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : UnitBehavior
{
    GameObject wayPoint;
    protected override void Awake()
    {
        base.Awake();
        // init a list of waypoints at first, and remove the ones it reaches later
        List<GameObject> wayPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));
        wayPoint = FindClosest(wayPoints);
    }

    protected override void PerformAlert()
    {

        currentAttackTarget = FindPossibleAttackTargetInRange();
        if (currentAttackTarget == null)
        {
            // if there is no target to attack, go to the closest waypoint
            agent.SetDestination(wayPoint.transform.position);
            if (Vector3.Distance(transform.position, wayPoint.transform.position) < 2.5)
            {
                // player lose health
                FindObjectOfType<LevelManager>().LoseHealth(1);
                Destroy(gameObject);
            }
        }
        else
        {
            FaceTarget(currentAttackTarget.transform.position);
            currentState = State.CHASE;
        }
    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack()
    {
        agent.isStopped = true;
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
            FaceTarget(currentAttackTarget.transform.position);
            Attack(currentAttackTarget);
            lastAttackDeltaTime = 0;
        }
        
    }

}
