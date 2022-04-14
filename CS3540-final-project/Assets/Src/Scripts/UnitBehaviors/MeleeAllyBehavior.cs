using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAllyBehavior : AllyBehavior
{
    protected override void UpdateState()
    {
        switch (currentState)
        {
            case State.IDLE:
                anim.SetInteger("animState", 0);
                break;
            case State.ALERT:
                anim.SetInteger("animState", 0);
                PerformAlert();
                break;
            case State.CHASE:
                anim.SetInteger("animState", 2);
                PerformChase();
                break;
            case State.ATTACK:
                PerformAttack();
                break;
            case State.DIE:
                anim.SetInteger("animState", 5);
                PerformDie();
                break;
            default:
                break;
        }

    }


    public override void Attack(GameObject target)
    {
        target.GetComponent<MeleeEnemyBehavior>().TakeDamage(attackDamage, gameObject);

    }
    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Enemy" });
        GameObject closest = FindClosest(possibleTargets);
        return closest;
    }
}
