using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DogBehavior : EnemyBehavior
{
    private int startHealth = 80;
    GameObject currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        moveSpeed = 10f;
        attackRange = 5f;
        attackDamage = 10;
        allTarget = GameObject.FindGameObjectsWithTag("Unit");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
