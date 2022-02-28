using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAllyBehavior : AllyBehavior
{
    public override void Attack(GameObject target)
    {
        target.GetComponent<MeleeEnemyBehavior>().TakeDamage(attackDamage);
    }
    public override GameObject FindPossibleTarget()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Enemy" });
        GameObject closest = FindClosest(possibleTargets);
        return closest;
    }
}
