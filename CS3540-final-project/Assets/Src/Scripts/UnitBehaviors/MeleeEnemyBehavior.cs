using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemyBehavior : EnemyBehavior
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
        target.GetComponent<MeleeAllyBehavior>().TakeDamage(attackDamage, gameObject);
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Ally" });
        GameObject closest = FindClosest(possibleTargets);
        return closest;
    }
}
