using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UnitBehavior : MonoBehaviour, UnitInterface
{
    public int attackRange;
    public int moveSpeed;
    public int health;
    int attackAmount;

    public void TakeDamage(int attackAmount)
    {
    }

    public void MoveTowardTarget(GameObject target)
    {
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

}