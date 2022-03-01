using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : UnitBehavior
{
    protected List<GameObject> wayPoints;

    protected override void Awake()
    {
        base.Awake();
        // init a list of waypoints at first, and remove the ones it reaches later
        wayPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));
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
        // if there is no target to attack, go to the closest waypoint
        else
        {
            GameObject wayPoint = FindClosest(wayPoints);
            if (wayPoint != null)
            {
                MoveTowardTarget(wayPoint.transform);
                if (Vector3.Distance(transform.position, wayPoint.transform.position) < 1)
                {
                    wayPoints.Remove(wayPoint);
                }
            }
            else
            {
                FindObjectOfType<LevelManager>().LoseHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
