using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAllyBehavior : AllyBehavior
{
    public override void Attack(GameObject target)
    {
        int picker = Random.Range(0, 2);
        if (picker == 0)
        {
            anim.SetInteger("animState", ATTACK1_ANIM);
        }
        else
        {
            anim.SetInteger("animState", ATTACK2_ANIM);
        }
        target.GetComponent<MeleeEnemyBehavior>().TakeDamage(attackDamage, gameObject);
    }
    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Enemy" });
        GameObject closest = FindClosest(possibleTargets);
        return closest;
    }
}
