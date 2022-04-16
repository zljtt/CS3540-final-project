using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("FireProjectile")) {
            //print(this.gameObject.name + " hit by fire ball");
            var  methodClass = gameObject.GetComponent<UnitBehavior>();
            methodClass.TakeDamage(10, other.gameObject);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("MagicProjectile")) {
            var  methodClass = gameObject.GetComponent<UnitBehavior>();
            methodClass.TakeDamage(5, other.gameObject);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("EnemyPoisonProjectile")) {
            if(this.tag == "Ally" || this.tag == "Dragon") {
                var  methodClass = gameObject.GetComponent<UnitBehavior>();
                methodClass.TakeDamage(5, other.gameObject);
                Destroy(other.gameObject);
            }
            else if(this.tag == "Enemy") {
                var  methodClass = gameObject.GetComponent<UnitBehavior>();
                methodClass.Heal(5);
                Destroy(other.gameObject);
            }
        }
    }
}
