using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UnitBehavior : MonoBehaviour
{
    public Slider healthSlider1;
    public Slider healthSlider2;
    public int maxHealth = 100;
    public float attackRange = 2f;
    public float alertRange = 4f;
    public int attackDamage = 2;
    public float moveSpeed = 2f;

    protected GameObject currentTarget;
    protected float currentHealth;
    private float invincibilityFrameCountDown = 1f;

    void Start()
    {
        healthSlider1.maxValue = maxHealth;
        healthSlider2.maxValue = maxHealth;
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        if (invincibilityFrameCountDown > 0)
        {
            invincibilityFrameCountDown -= Time.deltaTime;
        }
        healthSlider1.value = currentHealth;
        healthSlider2.value = currentHealth;
    }

    public abstract void Attack(GameObject target);

    public abstract GameObject FindPossibleTarget();

    protected List<GameObject> FindTargetsInRange(List<string> tags)
    {
        List<GameObject> allTarget = new List<GameObject> { };
        for (int i = 0; i < tags.Count; i++)
        {
            List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag(tags[i]));
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < alertRange)
                {
                    allTarget.Add(target);
                }
            }
        }
        return allTarget;
    }

    protected GameObject FindClosest(List<GameObject> targets)
    {
        GameObject closest = null;
        float smallestDistance = Mathf.Infinity;
        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < smallestDistance)
            {
                closest = target;
                smallestDistance = distance;
            }
        }
        return closest;
    }

    public void MoveTowardTarget(GameObject target)
    {
        float step = moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    public bool CanReach(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount > currentHealth)
        {
            UnitDies();
        }
        else if (invincibilityFrameCountDown <= 0)
        {
            currentHealth -= damageAmount;
            invincibilityFrameCountDown = 0.1f;
        }
    }

    public void UnitDies()
    {
        //may need to add sound effect and game lose or win condition 
        Destroy(gameObject);
    }

    public Slider GetSlider()
    {
        return healthSlider1;
    }
}
