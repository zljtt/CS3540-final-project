using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackAllyBehavior : AllyBehavior
{
    public AudioClip attackSFX;
    public GameObject firePrefab;
    public Transform shootPoint;

    public override void Attack(GameObject target)
    {
        anim.SetTrigger(ATTACK1_TRIGGER);
        AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
        shootPoint.LookAt(currentAttackTarget.transform);
        Instantiate(firePrefab, shootPoint.position, shootPoint.rotation);
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new string[] { "Enemy" });
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }



}
