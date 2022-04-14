using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemyBehavior : EnemyBehavior
{
    protected override void UpdateState()
    {
        switch (currentState)
        {
            case State.IDLE:
                anim.SetInteger("animState", 0);
                break;
            case State.ALERT:
                anim.SetInteger("animState", 1);
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
        int picker = Random.Range(0, 1);
        if(picker == 0) {
            anim.SetInteger("animState", 3);               
        }
        else {
            anim.SetInteger("animState", 4);
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
