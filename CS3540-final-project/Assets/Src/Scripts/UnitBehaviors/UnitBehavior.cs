using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public abstract class UnitBehavior : MonoBehaviour
{
    public enum State { INIT, IDLE, ALERT, CHASE, ATTACK, DIE };

    public int maxHealth = 100;
    public float attackRange = 2f;
    public float alertRange = 4f;
    public int attackDamage = 2;
    public float attackSpeed = 1f;

    private Slider[] healthSliders;
    protected NavMeshAgent agent;
    protected GameObject currentAttackTarget;
    protected Animator anim;
    protected float currentHealth;
    protected float lastDamagedDeltaTime = 0f; // use for invincibility frame
    protected float lastAttackDeltaTime = 0f; // use for attack speed
    protected State currentState;

    protected virtual void Awake()
    {
        currentState = State.INIT;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        healthSliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.maxValue = maxHealth;
        }
        currentHealth = maxHealth;
    }

    private void Update()
    {
        lastDamagedDeltaTime += Time.deltaTime;
        lastAttackDeltaTime += Time.deltaTime;
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.value = currentHealth;
        }
        UpdateState();
    }

    protected void FaceTarget(Vector3 target) {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case State.IDLE:
                break;
            case State.ALERT:
                PerformAlert();
                break;
            case State.CHASE:
                PerformChase();
                break;
            case State.ATTACK:
                PerformAttack();
                break;
            case State.DIE:
                PerformDie();
                break;
            default:
                break;
        }
    }
    // when an unit does not have a target, it enters alert state and wait for target
    protected virtual void PerformAlert()
    {
        // to implement in child
    }

    // when an unit has a target, it will chase the target until entering attack range
    protected virtual void PerformChase()
    {
        agent.isStopped = false;
        if (currentAttackTarget == null)
        {
            currentState = State.ALERT;
        }
        else if (CanReach(currentAttackTarget))
        {
            currentState = State.ATTACK;
            FaceTarget(currentAttackTarget.transform.position);
        }
        else // chase
        {
            agent.SetDestination(currentAttackTarget.transform.position);
            FaceTarget(currentAttackTarget.transform.position);
        }
    }

    // when an unit is within attack range, it attack until the target dies
    protected virtual void PerformAttack()
    {
        // to implement in child
    }

    // when an unit die
    protected virtual void PerformDie()
    {
        Destroy(gameObject, 2);
    }

    public abstract void Attack(GameObject target);

    // find a possible target within the alert range.
    public abstract GameObject FindPossibleAttackTargetInRange();

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

    public bool CanReach(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    public void TakeDamage(float damageAmount, GameObject source)
    {
        if (damageAmount > currentHealth)
        {
            currentState = State.DIE;
        }
        else if (lastDamagedDeltaTime > 0.1f)
        {
            currentHealth -= damageAmount;
            lastDamagedDeltaTime = 0;
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
    }

    public void ChangeState(State state)
    {
        currentState = state;
    }



}
