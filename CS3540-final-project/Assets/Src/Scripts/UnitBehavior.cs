using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UnitBehavior : MonoBehaviour, UnitAttack, UnitMove
{
    public int attackRange;
    public int moveSpeed;
    public int health;

    public void attack(GameObject target)
    {
    }

    public void moveTowardTarget(GameObject target)
    {
    }

}