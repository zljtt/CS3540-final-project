using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeEnemyBehavior : MonoBehaviour
{
    public int startHealth = 80;
    public float moveSpeed = 2f;
    public int attackDamage = 10;
    private int currentHealth;
    GameObject currentTarget;
    List<GameObject> allTarget = new List<GameObject>{};
    string attackStatus = "unit";

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        if(allTarget.Count == 0 ) {
            List<string> tags = new List<string> { "MeleeUnit", "FarAttackUnit" };
            initTargets(tags);
        }
        currentTarget = FindCloestTarget();
        Debug.Log("the cloest target for " + this.name + "is :" + currentTarget.name);
    }

    public void initTargets(List<string> tags){
        for(int i = 0; i < tags.Count; i++) {
            List<GameObject> targets = new List<GameObject> (
                GameObject. FindGameObjectsWithTag (tags[i]));
            allTarget.AddRange(targets);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardTarget(currentTarget);
    }
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("touched" + other.name);
    }
    void Attack() {
        /*
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
        */
    }

    public void changeTarget() {
        /*
        allTarget = GameObject.FindGameObjectsWithTag("Unit");
        if(allTarget == null) {
            attackStatus = "endpoint";
            currentTarget = GameObject.FindGameObjectWithTag("EndPoint");
        }
        else {
            attackStatus = "endpoint";
            currentTarget = GameObject.FindGameObjectWithTag("EndPoint");
        }
        */
    }

    public GameObject FindCloestTarget() {
        GameObject closest = null;
        float distance = Mathf.Infinity;

        for(int i = 0; i < allTarget.Count; i++) {
            GameObject target = allTarget[i];
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
