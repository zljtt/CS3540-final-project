using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackEnemyBehavior : MonoBehaviour
{
    public int startHealth = 80;
    private float moveSpeed = 2f;
    private float attackRange = 1f;
    private int attackDamage = 10;
    private int currentHealth;
    GameObject currentTarget;
    GameObject[] allTarget;
    string attackStatus = "unit";

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        if(allTarget == null) {
            allTarget = GameObject.FindGameObjectsWithTag("Unit");
        }
        currentTarget = FindCloestTarget();
        //Debug.Log("the cloest target for " + this.name + "is :" + currentTarget.name);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
        if(distance > attackRange) { 
            MoveTowardTarget(currentTarget);
        }
        else {
            Attack();
        }
    }
    
    void Attack() {
        if(attackStatus != "unit") {
            var behavior = currentTarget.GetComponent<EndPointBehavior>();
            behavior.TakeDamage(attackDamage);
            if(behavior.checkDeath()) {
                Debug.Log("Game end");
            }
        }
        else {
            var behavior = currentTarget.GetComponent<MeleeUnitBehavior>();
            behavior.TakeDamage(attackDamage);
            if(behavior.checkDeath()) {
                changeTarget();
                Debug.Log(currentTarget.name + " dies");
            }
        }
    }

    public void changeTarget() {
        allTarget = GameObject.FindGameObjectsWithTag("Unit");
        if(allTarget == null) {
            attackStatus = "endpoint";
            currentTarget = GameObject.FindGameObjectWithTag("EndPoint");
        }
        else {
            attackStatus = "endpoint";
            currentTarget = GameObject.FindGameObjectWithTag("EndPoint");
        }
    }

    public GameObject FindCloestTarget() {
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject target in allTarget) {
            Vector3 diff = target.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = target;
                distance = curDistance;
            }
        }
        return closest;
    }
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
