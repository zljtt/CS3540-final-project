using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    HOLY,
    OTHER
}
public class ProjectileBehavior : MonoBehaviour
{
    public GameObject shooter;
    public int attackDamage;
    public ProjectileType projectileType;

    private void damageTarget(Collider other)
    {
        var unit = other.GetComponent<UnitBehavior>();
        unit.TakeDamage(attackDamage, other.gameObject);
        Destroy(gameObject);
    }

    private void healTarget(Collider other)
    {
        var unit = other.GetComponent<UnitBehavior>();
        unit.Heal(attackDamage);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (projectileType == ProjectileType.HOLY)
        {
            if ((shooter.CompareTag("Ally") && other.CompareTag("Enemy")) || (shooter.CompareTag("Enemy") && other.CompareTag("Ally")))
            {
                damageTarget(other);
            }
            else if ((shooter.CompareTag("Ally") && other.CompareTag("Ally")) || (shooter.CompareTag("Enemy") && other.CompareTag("Enemy")))
            {
                healTarget(other);
            }
        }
        else
        {
            if ((shooter.CompareTag("Ally") && other.CompareTag("Enemy")) || (shooter.CompareTag("Enemy") && other.CompareTag("Ally")))
            {
                damageTarget(other);
            }
        }
    }
}
