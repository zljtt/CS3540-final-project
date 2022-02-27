using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitEnemyBehavior : MonoBehaviour
{
    protected int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Attack(GameObject target);
    public abstract List<GameObject> initTargets();

    public abstract GameObject FindDesiredTarget(List<GameObject> targets);

    public List<GameObject> FindAllTarget(List<string> tags, List<GameObject> exclude){
        List<GameObject> allTarget = new List<GameObject>{};
        for(int i = 0; i < tags.Count; i++) {
            List<GameObject> targets = new List<GameObject> (
                GameObject. FindGameObjectsWithTag (tags[i]));
            allTarget.AddRange(targets);
        }
        foreach(GameObject item in exclude) allTarget.Remove(item);
        return allTarget;
    }

    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
        }
        if(currentHealth <= 0) {
            UnitDies();
        }
    }
    public bool checkDeath(int damageAmount) {
        return currentHealth - damageAmount <= 0;
    }
    public void UnitDies() {
        //may need to add sound effect and game lose or win condition 
        Destroy(gameObject);
    }
}
