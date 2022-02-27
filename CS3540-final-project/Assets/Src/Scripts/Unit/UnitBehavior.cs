using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UnitBehavior : MonoBehaviour, UnitInterface
{
    protected int currentHealth;
    protected float attackRange;
    protected float attackDamage; 
    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
        }
        if(currentHealth <= 0) {
            UnitDies();
        }
    }

    public void UnitDies() {
        //may need to add sound effect and game lose or win condition 
    }
}