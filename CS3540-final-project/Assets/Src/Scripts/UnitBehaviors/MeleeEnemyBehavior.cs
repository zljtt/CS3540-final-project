using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemyBehavior : EnemyBehavior
{
    public AudioClip attackSFX;
    int currentAttackAnim = 0;
    public override void Attack(GameObject target)
    {
        if (currentAttackAnim == 0)
        {
            anim.SetInteger("animState", ATTACK1_ANIM);
            currentAttackAnim = 1;
        }
        else
        {
            anim.SetInteger("animState", ATTACK2_ANIM);
            currentAttackAnim = 0;
        }
        AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
        target.GetComponent<UnitBehavior>().TakeDamage(attackDamage, gameObject);
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Ally" });
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }
}
