using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarAttackEnemyBehavior : EnemyBehavior
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
            behavior.attackDamage = attackDamage;
            behavior.shooter = gameObject;
        }
    }


    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = new List<GameObject>();
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            var unit = target.GetComponent<UnitBehavior>();
            if (Vector3.Distance(transform.position, target.transform.position) < alertRange &&
                unit.GetHealth() < unit.maxHealth && !unit.ContainType(UnitType.HEAL))
            {
                possibleTargets.Add(target);
            }
        }
        if (possibleTargets.Count > 0)
        {
            return FindClosest(transform, possibleTargets);
        }
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Ally"))
        {
            if (Vector3.Distance(transform.position, target.transform.position) < alertRange)
            {
                possibleTargets.Add(target);
            }
        }
        return FindClosest(transform, possibleTargets);
    }
}
