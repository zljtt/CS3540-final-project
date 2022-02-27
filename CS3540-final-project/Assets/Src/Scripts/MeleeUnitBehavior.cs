using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeUnitBehavior : UnitEnemyBehavior {
    
    public Slider healthSlider1;
    public Slider healthSlider2;
    public int startHealth = 80;
    public float attackRange = 2f;
    public int attackDamage = 3;
    private int currentHealth;
    GameObject currentTarget;
    private void Awake() {
        healthSlider1.maxValue = startHealth;
        healthSlider2.maxValue = startHealth;
    }
    void Start()
    {
        currentHealth = startHealth;
        healthSlider1.value = currentHealth;
        healthSlider2.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.name + " current health: " + currentHealth);
        List<GameObject> targets = FindCloestTarget();
        if(targets.Count > 0) {
            currentTarget = targets[0];
            Attack(currentTarget);
        }
    }

    void Attack(GameObject target) {
        var behavior = target.GetComponent<MeleeEnemyBehavior>();
        behavior.TakeDamage(attackDamage);
    }

    public void TakeDamage(int damageAmount) {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
            healthSlider1.value = currentHealth;
            healthSlider2.value = currentHealth;
        }
        if(currentHealth <= 0) {
            UnitDies();
        }
    }
    public List<GameObject> FindCloestTarget() {
        List<string> tags = new List<string> { "MeleeEnemy", "FarAttackEnemy"};
        List<GameObject> exclude = new List<GameObject> {};
        List<GameObject> allTarget = findAllTarget(tags, exclude);
        List<GameObject> closeTarget = new List<GameObject> {};

        for(int i = 0; i < allTarget.Count; i++) {
            GameObject target = allTarget[i];
            float diff = Vector3.Distance(target.transform.position, transform.position);
            if (diff < attackRange)
            {
                closeTarget.Add(target);
            }
        }
        return closeTarget;
    }

    public List<GameObject> findAllTarget(List<string> tags, List<GameObject> exclude){
        List<GameObject> allTarget = new List<GameObject>{};
        for(int i = 0; i < tags.Count; i++) {
            List<GameObject> targets = new List<GameObject> (
                GameObject. FindGameObjectsWithTag (tags[i]));
            allTarget.AddRange(targets);
        }
        foreach(GameObject item in exclude) allTarget.Remove(item);
        return allTarget;
    }

    public bool checkDeath(int damageAmount) {
        return currentHealth - damageAmount <= 0;
    }

    void UnitDies() {
        Destroy(gameObject);
    }
}
