using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarAttackEnemyBehavior : EnemyBehavior
{
    public AudioClip attackSFX;
    public GameObject firePrefab;
    public Transform shootPoint;


    protected override void PerformAttack() {
        anim.SetInteger("animState", ATTACK2_ANIM);
        if (currentAttackTarget == null || !CanReach(currentAttackTarget))
        {
            currentState = State.ALERT;
        }
        else if (lastAttackDeltaTime > attackSpeed) // attack
        {
            agent.SetDestination(currentAttackTarget.transform.position); // keep rotation
            Attack(currentAttackTarget);
            lastAttackDeltaTime = 0;
        }
        else {
            anim.SetInteger("attackState", 0);
        }
    }

    public override void Attack(GameObject target)
    {
        anim.SetInteger("attackState", 1);
        AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
        shootPoint.LookAt(currentAttackTarget.transform);
        Instantiate(firePrefab, shootPoint.position, shootPoint.rotation);
    }
    
    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Ally", "Enemy"});
        possibleTargets.Remove(this.gameObject);
        GameObject lowestHealthTarget = FindLowestHealth(transform, possibleTargets);
        return lowestHealthTarget;
    }

    public GameObject FindLowestHealth(Transform transform, List<GameObject> targets)
    {
        GameObject lowestHealthTarget = null;
        float lowestHealth = Mathf.Infinity;
        foreach (GameObject target in targets)
        {
            var methodClass = target.GetComponent<UnitBehavior>();
            float currentHealth = methodClass.returnHealth();

            if (currentHealth < lowestHealth)
            {
                lowestHealthTarget = target;
                lowestHealth = currentHealth;
            }
        }
        return lowestHealthTarget;
    }
}
