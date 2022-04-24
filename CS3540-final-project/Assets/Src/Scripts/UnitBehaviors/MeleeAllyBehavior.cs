using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAllyBehavior : AllyBehavior
{
    public AudioClip attackSFX;
    public float critChance = 0.2f;
    public override void Attack(GameObject target)
    {
        AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
        if (Random.Range(0.0f, 1.0f) >= critChance)
        {
            target.GetComponent<UnitBehavior>().TakeDamage(attackDamage, gameObject);
            anim.SetTrigger(ATTACK1_TRIGGER);
        }
        else
        {
            target.GetComponent<UnitBehavior>().TakeDamage(attackDamage * 2, gameObject);
            anim.SetTrigger(ATTACK2_TRIGGER);
        }
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new string[] { "Enemy" }, UnitType.FLY);
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }
}
