using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnitBehavior : MonoBehaviour {
    
    public int startHealth = 100;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.name + " current health: " + currentHealth);
    }

    public void TakeDamage(int damageAmount) {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
        }
        if(currentHealth <= 0) {
            UnitDies();
        }
    }

    public bool checkDeath(int damageAmount) {
        return currentHealth - damageAmount <= 0;
    }

    void UnitDies() {
        Destroy(gameObject, 1);
    }
}
