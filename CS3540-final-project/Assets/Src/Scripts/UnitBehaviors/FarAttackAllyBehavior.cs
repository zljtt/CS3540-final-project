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
        Invoke("Shoot", 0.5f);
    }

    public void Shoot()
    {
        if (currentAttackTarget != null)
        {
            AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
            Vector3 offset = new Vector3(0, 0.5f, 0);
            shootPoint.position -= offset;
            shootPoint.LookAt(currentAttackTarget.transform);
            shootPoint.position += offset;
            GameObject projectile = Instantiate(firePrefab, shootPoint.position, shootPoint.rotation);
            var behavior = projectile.GetComponent<ProjectileBehavior>();
            behavior.attackDamage = GetAttackDamage();
            behavior.shooter = gameObject;
        }
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new string[] { "Enemy" });
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }



}
