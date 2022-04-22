using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public string attackTag;
    public string currentTag;
    public int attackDamage;
    public string projectileType;
    bool isPoison = false;

    // Start is called before the first frame update
    void Start()
    {
        if(projectileType == "Poison") {
            isPoison = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void attackTarget(Collider other) {
        var  methodClass = other.GetComponent<UnitBehavior>();
        methodClass.TakeDamage(attackDamage, other.gameObject);
        Destroy(gameObject);
    }

    private void poisonBehavior(Collider other) {
        if(currentTag == "Enemy") {
            if(other.CompareTag("Ally")) {
                attackTarget(other);
            }
            else if(other.CompareTag("Enemy")) {
                var  methodClass = gameObject.GetComponent<UnitBehavior>();
                methodClass.Heal(5);
                Destroy(gameObject);
            }
        }
        else if(currentTag == "Ally") {
            if(other.CompareTag("Enemy")) {
                attackTarget(other);
            }
            else if(other.CompareTag("Ally")) {
                var  methodClass = gameObject.GetComponent<UnitBehavior>();
                print("hitted");
                methodClass.Heal(5);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(attackTag)) {
            if(isPoison) {
                poisonBehavior(other);
            }
            else {
                attackTarget(other);
            }
        }
    }
}
