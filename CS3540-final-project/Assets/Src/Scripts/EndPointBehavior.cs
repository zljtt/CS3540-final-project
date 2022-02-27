using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentHealth;
    public int startHealth = 100;
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount) {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
        }
        if(currentHealth <= 0) {
            Debug.Log("game end");
        }
    }

    public bool checkDeath() {
        return currentHealth <= 0;
    }
}
