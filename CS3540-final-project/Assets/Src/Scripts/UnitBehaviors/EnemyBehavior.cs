using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : UnitBehavior
{
    protected List<GameObject> wayPoints;

    private void Awake()
    {
        wayPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));
    }

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
            GameObject wayPoint = FindClosest(wayPoints);
            if (wayPoint != null)
            {
                MoveTowardTarget(wayPoint);
                if (Vector3.Distance(transform.position, wayPoint.transform.position) < 1)
                {
                    wayPoints.Remove(wayPoint);
                }
            }
            else
            {
                Destroy(gameObject);
                // reduce player health in level manager
            }
        }
    }
}
