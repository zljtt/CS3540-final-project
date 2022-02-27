using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyInterface
{
    public void MoveTowardTarget(GameObject target);
    public void TakeDamage(int damageAmount);
    public void EnemyDies();
}
