using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathIndicaterBehavior : MonoBehaviour
{
    public Vector3 target;
    void Start()
    {
        // init a list of waypoints at first, and remove the ones it reaches later
        List<GameObject> wayPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));
        GameObject wayPoint = UnitBehavior.FindClosest(transform, wayPoints);
        target = new Vector3(wayPoint.transform.position.x, 0, wayPoint.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(target);
        if (Vector3.Distance(transform.position, target) < 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
