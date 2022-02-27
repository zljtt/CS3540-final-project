using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour, EnemyInterface
{
    protected int currentHealth;
    protected float moveSpeed;
    protected float attackRange;
    protected float attackDamage; 
    protected GameObject[] allTarget;

    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
        }
        if(currentHealth <= 0) {
            EnemyDies();
        }
    }

    public void MoveTowardTarget(GameObject target)
    {
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
    public void EnemyDies() {
        //may need to add sound effect and game lose or win condition 
    }
}
