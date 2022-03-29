using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public abstract class UnitBehavior : MonoBehaviour
{
    public Slider healthSlider1;
    public Slider healthSlider2;
    public int maxHealth = 100;
    public float attackRange = 2f;
    public float alertRange = 4f;
    public int attackDamage = 2;
    public float attackSpeed = 1f;
    protected NavMeshAgent agent;
    protected GameObject currentTarget;
    protected float currentHealth;
    protected float lastDamagedDeltaTime = 0f; // use for invincibility frame
    protected float lastAttackDeltaTime = 0f; // use for attack speed
    protected bool active;
    Animator anim;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        healthSlider1.maxValue = maxHealth;
        healthSlider2.maxValue = maxHealth;
        currentHealth = maxHealth;
        active = false;
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        lastDamagedDeltaTime += Time.deltaTime;
        lastAttackDeltaTime += Time.deltaTime;
        healthSlider1.value = currentHealth;
        healthSlider2.value = currentHealth;
    }

    public abstract void Attack(GameObject target);

    // find a possible target within the alert range.
    public abstract GameObject FindPossibleAttackTarget();

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

    public void MoveTowardTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }

    public bool CanReach(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    public void TakeDamage(float damageAmount, GameObject source)
    {
        if (damageAmount > currentHealth)
        {
            UnitDies();
        }
        else if (lastDamagedDeltaTime > 0.1f)
        {
            currentHealth -= damageAmount;
            lastDamagedDeltaTime = 0;
        }
    }

    public void TakeHealth(float healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
        }
    }

    public void UnitDies()
    {
        //may need to add sound effect and game lose or win condition 
        anim.SetInteger("status", 1);
        Destroy(gameObject, 1);
    }

    public Slider GetSlider()
    {
        return healthSlider1;
    }

    public void SetActive(bool a)
    {
        active = a;
    }

}
