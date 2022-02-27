using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemyBehavior : UnitEnemyBehavior
{
    public Slider healthSlider1;
    public Slider healthSlider2;
    public int startHealth = 70;
    public float attackRange = 2f;
    public int attackDamage = 2;
    
    public float moveSpeed = 2f;
    GameObject currentTarget;
    List<GameObject> touchedWayPoint = new List<GameObject>{};
    string attackStatus = "unit";

    private void Awake() {
        healthSlider1.maxValue = startHealth;
        healthSlider2.maxValue = startHealth;
        currentHealth = startHealth;
    }

    void Start()
    {
    }

    void Update()
    {
        healthSlider1.value = currentHealth;
        healthSlider2.value = currentHealth;
        List<GameObject> targets = initTargets();
        if(targets.Count > 0) {
            currentTarget = FindDesiredTarget(targets);
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
            if(distance > attackRange) { 
                MoveTowardTarget(currentTarget);
            }
            else {
                if(currentTarget.tag != "Waypoint") {  
                    Attack(currentTarget);
                }
                else {
                    touchedWayPoint.Add(currentTarget);
                }
            }
        }
    }

    public override List<GameObject> initTargets() {
        List<string> tags = new List<string> { "MeleeUnit", "FarAttackUnit", "Waypoint"};
        List<GameObject> tempTarget = FindAllTarget(tags, touchedWayPoint);
        return tempTarget;
    }

    public override void Attack(GameObject target) {
        if(attackStatus != "unit") {
            var behavior = target.GetComponent<EndPointBehavior>();
            behavior.TakeDamage(attackDamage);
        }
        else {
            var behavior = target.GetComponent<UnitEnemyBehavior>();
            behavior.TakeDamage(attackDamage);
        }
    }

    public override GameObject FindDesiredTarget(List<GameObject> targets) {
        GameObject closest = null;
        float distance = Mathf.Infinity;

        for(int i = 0; i < targets.Count; i++) {
            GameObject target = targets[i];
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


    public void MoveTowardTarget(GameObject target)
    {
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    public Slider GetSlider() {
        return healthSlider1;
    }
}
