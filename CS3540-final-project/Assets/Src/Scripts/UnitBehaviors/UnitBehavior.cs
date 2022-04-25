using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public enum UnitType
{
    FLY,
    GROUND,
    RANGED,
    MELEE,
    HEAL,
}
public abstract class UnitBehavior : MonoBehaviour
{
    public static readonly int IDLE_ANIM = 0;
    public static readonly int WALK_ANIM = 1;
    public static readonly int RUN_ANIM = 2;
    public static readonly int ATTACK_ANIM = 3;

    public static readonly string ATTACK1_TRIGGER = "attack1";
    public static readonly string ATTACK2_TRIGGER = "attack2";

    public static readonly int OTHER_ANIM = 5;
    public List<UnitType> types;
    public static readonly int DIE_ANIM = -1;
    public enum State { IDLE, ALERT, CHASE, ATTACK, DIE };
    public AudioClip healSFX;
    public int level;

    public int maxHealth = 100;
    public float attackRange = 2f;
    public float alertRange = 4f;
    public int attackDamage = 2;
    public float attackSpeed = 1f;
    public float moveSpeed = 2;

    protected int modifiedMaxHealth = -1;
    protected float modifiedAttackRange = -1;
    protected float modifiedAlertRange = -1;
    protected float modifiedAttackDamage = -1;
    protected float modifiedAttackSpeed = -1;
    protected float modifiedMoveSpeed = -1;

    protected Slider[] healthSliders;
    protected NavMeshAgent agent;
    public GameObject currentAttackTarget;
    protected Animator anim;
    protected float currentHealth;
    protected float lastDamagedDeltaTime = 0f; // use for invincibility frame
    protected float lastAttackDeltaTime = 0f; // use for attack speed
    protected float effectDeltaTime = 0f; // use for attack speed
    public State currentState;
    protected Transform playerPosition;
    private Dictionary<EffectType, int> effects = new Dictionary<EffectType, int>();
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
        effectDeltaTime += Time.deltaTime;
        if (effectDeltaTime >= 1)
        {
            EffectType[] effectTypes = new EffectType[effects.Count];
            effects.Keys.CopyTo(effectTypes, 0);
            foreach (EffectType effect in effectTypes)
            {
                effects[effect] -= 1;
                if (effects[effect] <= 0)
                {
                    effects.Remove(effect);
                }
            }
            // restore stats
            modifiedMaxHealth = maxHealth;
            modifiedAlertRange = alertRange;
            modifiedAttackDamage = attackDamage;
            modifiedAttackRange = attackRange;
            modifiedAttackSpeed = attackSpeed;
            modifiedMoveSpeed = moveSpeed;
            UpdateEffectPerSecond();
            effectDeltaTime -= 1;
        }
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.value = currentHealth;
        }
        UpdateState();
    }
    protected virtual void UpdateEffectPerSecond()
    {
        if (effects.ContainsKey(EffectType.HEALING))
        {
            Heal(modifiedMaxHealth * 0.05f);
        }
        if (effects.ContainsKey(EffectType.RAGE))
        {
            TakeDamage(5, null);
            modifiedAttackDamage = modifiedAttackDamage * 1.25f;
            modifiedAttackSpeed = modifiedAttackSpeed * 0.75f;

        }
        if (effects.ContainsKey(EffectType.ENCOURAGE))
        {
            modifiedAttackDamage = modifiedAttackDamage * 1.2f;
            modifiedMaxHealth = (int)(modifiedMaxHealth * 1.2f);
        }
        if (effects.ContainsKey(EffectType.WINDGRACE))
        {
            modifiedMoveSpeed = modifiedMoveSpeed * 2f;
            modifiedAlertRange = modifiedAlertRange * 1.5f;
        }
    }
    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case State.IDLE:
                PerformIdle();
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
    protected virtual void PerformIdle()
    {
        anim.SetInteger("animState", IDLE_ANIM);
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

    public List<GameObject> FindTargetsInRange(string[] tags, params UnitType[] ignore)
    {
        List<GameObject> allTarget = new List<GameObject> { };
        for (int i = 0; i < tags.Length; i++)
        {
            List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag(tags[i]));
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < GetAlertRange()
                    && !target.GetComponent<UnitBehavior>().ContainType(ignore))
                {
                    allTarget.Add(target);
                }
            }
        }
        return allTarget;
    }
    public bool ContainType(params UnitType[] targetTypes)
    {
        foreach (UnitType type in targetTypes)
        {
            foreach (UnitType contain in types)
            {
                if (type == contain)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public static GameObject FindClosest(Transform transform, List<GameObject> targets)
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
        return Vector3.Distance(transform.position, target.transform.position) <= GetAttackRange();
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
        GameObject effect = GameObject.Instantiate(Resources.Load("Prefabs/Effects/HealEffect"), transform.position, Quaternion.Euler(-90, 0, 90)) as GameObject;
        effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        effect.transform.parent = transform;
        AudioSource.PlayClipAtPoint(healSFX, playerPosition.position);
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, modifiedMaxHealth);
    }

    public void ChangeState(State state)
    {
        currentState = state;
    }
    public float GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        if (modifiedMaxHealth < 0)
        {
            return maxHealth;
        }
        return modifiedMaxHealth;
    }

    public int GetAttackDamage()
    {
        if (modifiedMaxHealth < 0)
        {
            return maxHealth;
        }
        return (int)modifiedAttackDamage;
    }

    public float GetAlertRange()
    {
        if (modifiedAlertRange < 0)
        {
            return alertRange;
        }
        return modifiedAlertRange;
    }
    public float GetAttackSpeed()
    {
        if (modifiedAttackSpeed < 0)
        {
            return attackSpeed;
        }
        return modifiedAttackSpeed;
    }

    public float GetAttackRange()
    {
        if (modifiedAttackRange < 0)
        {
            return attackRange;
        }
        return modifiedAttackRange;
    }
    public float GetMoveSpeed()
    {
        if (modifiedMoveSpeed < 0)
        {
            return moveSpeed;
        }
        return modifiedMoveSpeed;
    }
    public virtual bool TargetInSight()
    {
        return Vector3.Angle(transform.forward, (currentAttackTarget.transform.position - transform.position)) < 20f;
    }

    public void ApplyEffect(EffectType effect, int length)
    {
        effects[effect] = length;
    }
}