using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemyBehavior : EnemyBehavior
{
    public override void Attack(GameObject target)
    {
        target.GetComponent<MeleeAllyBehavior>().TakeDamage(attackDamage);
    }

    public override GameObject FindPossibleTarget()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Ally" });
        GameObject closest = FindClosest(possibleTargets);
        return closest;
    }
}
