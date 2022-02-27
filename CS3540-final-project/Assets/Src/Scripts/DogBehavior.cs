using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DogBehavior : MonoBehaviour
{
    public int startHealth = 80;
    private float moveSpeed = 2f;
    private float attackRange = 5f;
    private int attackDamage = 10;
    private int currentHealth;
    GameObject currentTarget;
    GameObject[] allTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        if(allTarget == null) {
            allTarget = GameObject.FindGameObjectsWithTag("Unit");
        }
        currentTarget = FindCloestTarget();
        Debug.Log("the cloest target for " + this.name + "is :" + currentTarget.name);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardTarget(currentTarget);
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
