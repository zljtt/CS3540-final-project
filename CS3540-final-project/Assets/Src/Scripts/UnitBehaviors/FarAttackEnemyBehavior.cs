using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarAttackEnemyBehavior : EnemyBehavior
{
    public AudioClip attackSFX;
    public GameObject firePrefab;
    public Transform shootPoint;

    public override void Attack(GameObject target)
    {
        anim.SetTrigger(ATTACK1_TRIGGER);
        AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
        shootPoint.LookAt(currentAttackTarget.transform);
        Instantiate(firePrefab, shootPoint.position, shootPoint.rotation);
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new string[] { "Ally", "Enemy" });
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
            float currentHealth = methodClass.GetHealth();

            if (currentHealth < lowestHealth)
            {
                lowestHealthTarget = target;
                lowestHealth = currentHealth;
            }
        }
        return lowestHealthTarget;
    }
}
