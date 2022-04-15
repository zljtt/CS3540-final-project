using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAllyBehavior : AllyBehavior
{
    public AudioClip attackSFX;
    public override void Attack(GameObject target)
    {
        AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
        anim.SetInteger("animState", ATTACK2_ANIM);
        target.GetComponent<MeleeEnemyBehavior>().TakeDamage(attackDamage, gameObject);
    }
    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Enemy" });
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }
}
