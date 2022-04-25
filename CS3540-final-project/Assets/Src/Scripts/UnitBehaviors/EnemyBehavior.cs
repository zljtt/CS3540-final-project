using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : UnitBehavior
{
    private GameObject wayPoint;
    public List<LootEntry> lootTable;
    private bool isQuitting = false;
    protected override void Start()
    {
        // init a list of waypoints at first, and remove the ones it reaches later
        List<GameObject> wayPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));
        wayPoint = FindClosest(transform, wayPoints);
        maxHealth = (int)Mathf.Floor(maxHealth * (1 + level * 0.1f));
        attackDamage = (int)Mathf.Floor(attackDamage * (1 + level * 0.1f));
        base.Start();
    }

    protected override void Update()
    {
        if (agent != null && agent.enabled)
        {
            agent.stoppingDistance = 0.2f;
            agent.speed = currentState == State.CHASE ? moveSpeed : moveSpeed * 1.5f;
            agent.isStopped = currentState == State.IDLE || currentState == State.DIE || currentState == State.ATTACK;
        }
        base.Update();
    }

    protected override void PerformAlert()
    {
        anim.SetInteger("animState", WALK_ANIM);
        currentAttackTarget = FindPossibleAttackTargetInRange();
        if (currentAttackTarget == null)
        {
            // if there is no target to attack, go to the closest waypoint
            agent.SetDestination(wayPoint.transform.position);
            if (Vector3.Distance(transform.position, wayPoint.transform.position) < 2)
            {
                // player lose health
                LevelManager.playerData.LoseHealth(1);
                Destroy(gameObject);
            }
        }
        else
        {
            //FaceTarget(currentAttackTarget.transform.position);
            currentState = State.CHASE;
        }
    }

    // when an unit has a target, it will chase the target until entering attack range
    protected override void PerformChase()
    {
        anim.SetInteger("animState", RUN_ANIM);
        if (currentAttackTarget == null)
        {
            currentState = State.ALERT;
        }
        else if (CanReach(currentAttackTarget) && TargetInSight())
        {
            currentState = State.ATTACK;
        }
        else if (agent != null && agent.enabled)
        {
            agent.SetDestination(currentAttackTarget.transform.position);
        }
    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack()
    {
        anim.SetInteger("animState", ATTACK_ANIM);
        if (currentAttackTarget == null || !CanReach(currentAttackTarget))
        {
            currentState = State.ALERT;
        }
        else if (!TargetInSight())
        {
            currentState = State.CHASE;
        }
        else if (lastAttackDeltaTime > attackSpeed) // attack
        {
            Attack(currentAttackTarget);
            lastAttackDeltaTime = 0;
        }
    }

    protected override void PerformDie()
    {
        anim.SetInteger("animState", DIE_ANIM);
        Destroy(gameObject, 1);
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {
            foreach (LootEntry loot in lootTable)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-0.4f, 0.4f), 0, Random.Range(-0.4f, 0.4f));
                loot.GenerateLoot(transform.position + randomOffset, 1 + level / 5, level * 0.1f);
            }
        }
    }
}
