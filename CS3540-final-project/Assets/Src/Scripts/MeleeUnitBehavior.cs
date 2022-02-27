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
    GameObject currentTarget;
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
            Attack(currentTarget);
        }
    }

    public override List<GameObject> initTargets() {
        List<string> tags = new List<string> { "MeleeEnemy", "FarAttackEnemy"};
        List<GameObject> exclude = new List<GameObject> {};
        List<GameObject> allTarget = FindAllTarget(tags, exclude);
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


    public override void Attack(GameObject target) {
        var behavior = target.GetComponent<UnitEnemyBehavior>();
        behavior.TakeDamage(attackDamage);
    }

    public override GameObject FindDesiredTarget(List<GameObject> targets) {
        float minHealth = 1000;
        GameObject resultTarget = null;
        for(int i = 0; i < targets.Count; i++) {
            GameObject target = targets[i];
            var behavior = target.GetComponent<MeleeEnemyBehavior>();
            Slider targetHealth = behavior.GetSlider();
            if(targetHealth.value < minHealth) {
                minHealth = targetHealth.value;
                resultTarget = target;
            }
        }
        return resultTarget;
    }
}
