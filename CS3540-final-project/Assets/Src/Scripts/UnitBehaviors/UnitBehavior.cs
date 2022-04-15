using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public abstract class UnitBehavior : MonoBehaviour
{
    public static readonly int IDLE_ANIM = 0;
    public static readonly int WALK_ANIM = 1;
    public static readonly int RUN_ANIM = 2;
    public static readonly int ATTACK1_ANIM = 3;
    public static readonly int ATTACK2_ANIM = 4;
    public static readonly int DIE_ANIM = -1;
    public enum State { IDLE, ALERT, CHASE, ATTACK, DIE };
    public AudioClip healSFX;

    public int maxHealth = 100;
    public float attackRange = 2f;
    public float alertRange = 4f;
    public int attackDamage = 2;
    public float attackSpeed = 1f;
    public float moveSpeed = 2;

    private Slider[] healthSliders;
    protected NavMeshAgent agent;
    protected GameObject currentAttackTarget;
    protected Animator anim;
    protected float currentHealth;
    protected float lastDamagedDeltaTime = 0f; // use for invincibility frame
    protected float lastAttackDeltaTime = 0f; // use for attack speed
    protected State currentState;
    protected Transform playerPosition;

    protected virtual void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        healthSliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.maxValue = maxHealth;
        }
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        lastDamagedDeltaTime += Time.deltaTime;
        lastAttackDeltaTime += Time.deltaTime;
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.value = currentHealth;
        }
        UpdateState();
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
    protected abstract void PerformAlert();

    // when an unit has a target, it will chase the target until entering attack range
    protected abstract void PerformChase();

    // when an unit is within attack range, it attack until the target dies
    protected abstract void PerformAttack();

    // when an unit die
    protected abstract void PerformDie();

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
        AudioSource.PlayClipAtPoint(healSFX, playerPosition.position);
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
    }

    public void ChangeState(State state)
    {
        currentState = state;
    }



}
