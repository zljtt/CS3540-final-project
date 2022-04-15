using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackAllyBehavior : AllyBehavior
{
    public AudioClip attackSFX;

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
        target.GetComponent<MeleeEnemyBehavior>().TakeDamage(attackDamage, gameObject);
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Enemy" });
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }

    

}
