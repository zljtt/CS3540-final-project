using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonAI : UnitBehavior
{
    public static readonly int DRA_IDLE_ANIM = 0;
    public static readonly int DRA_RISE_ANIM = 1;
    public static readonly int DRA_WALK_ANIM = 2;
    public static readonly int DRA_CHASE_ANIME = 3;
    public static readonly int DRA_ATTACK_ANIM = 4;
    public static readonly int DRA_DIE_ANIM = 5;
    public Transform shootPoint;

    public enum DragonState { IDLE, RISE, ALERT, CHASE, ATTACK, DIE}
    DragonState currentDragonState;
    float currentHeight = -0.5f;
    public float destinyHeight = 3;
    Vector3 currentPosition;
    public AudioClip attackSFX;
    public GameObject firePrefab;

    protected override void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        healthSliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.maxValue = maxHealth;
        }
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        currentPosition = this.gameObject.transform.position;
        currentPosition.y = currentHeight;
        this.gameObject.transform.position = currentPosition;
        currentDragonState = DragonState.RISE;
    }


    private void FixedUpdate() {
        currentPosition = this.gameObject.transform.position;
        currentHeight = currentPosition.y;
        if(currentAttackTarget != null) {
            FaceTarget(currentAttackTarget.transform.position);
        }
    }

    protected override void UpdateState()
    {
        switch (currentDragonState)
        {
            case DragonState.IDLE:
                break;
            case DragonState.RISE:
                PerformRise();
                break;
            case DragonState.ALERT:
                PerformAlert();
                break;
            case DragonState.CHASE:
                PerformChase();
                break;
            case DragonState.ATTACK:
                PerformAttack();
                break;
            case DragonState.DIE:
                PerformDie();
                break;
            default:
                break;
        }
    }
    void PerformRise() {
        if(currentHeight <= 0) {
            anim.SetInteger("animState", DRA_RISE_ANIM);
        }
        else {
            anim.SetInteger("animState", DRA_IDLE_ANIM);
        }
        if(currentHeight < destinyHeight) {
            Vector3 newPos = new Vector3(currentPosition.x, currentPosition.y + 0.05f, currentPosition.z);
            this.gameObject.transform.position = newPos;
        }
        else {
            currentDragonState = DragonState.IDLE;
        }
    }

        // when an unit does not have a target, it enters alert state and wait for target
    protected override void PerformAlert() {
        anim.SetInteger("animState", DRA_IDLE_ANIM);
        currentAttackTarget = FindPossibleAttackTargetInRange();
        if (currentAttackTarget != null) {
            currentDragonState = DragonState.CHASE;
        }
    }

    // when an unit has a target, it will chase the target until entering attack range
    protected override void PerformChase() {
        anim.SetInteger("animState", DRA_CHASE_ANIME);
        if(currentAttackTarget == null) {
            currentDragonState = DragonState.ALERT;
        }
        else if (CanReach(currentAttackTarget)) {
            currentDragonState = DragonState.ATTACK;
        }
        else {
            Vector3 targetPosition = currentAttackTarget.transform.position;
            targetPosition.y = currentHeight;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed *  Time.deltaTime);
        }

    }

    void FaceTarget(Vector3 target) {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack() {
        anim.SetInteger("animState", DRA_ATTACK_ANIM);
        if (currentAttackTarget == null || !CanReach(currentAttackTarget)) {
            currentDragonState = DragonState.ALERT;
        }
        else if (lastAttackDeltaTime > attackSpeed) // attack
        {
            Attack(currentAttackTarget);
            lastAttackDeltaTime = 0;
        }
        else {
            anim.SetInteger("attackState", 0);
        }
    }

    public override void Attack(GameObject target) { 
        anim.SetInteger("attackState", 1);
        AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
        RotateToDestination(gameObject, currentAttackTarget.transform.position);
        Instantiate(firePrefab, shootPoint.position, shootPoint.rotation);
    }

    void RotateToDestination(GameObject obj, Vector3 destination)
    {
        Vector3 direction = destination - obj.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }


    // when an unit die
    protected override void PerformDie() {
        anim.SetInteger("animState", DRA_DIE_ANIM);
        Destroy(gameObject, 2);
    }

    public void ChangeState(DragonState state) {
        currentDragonState = state;
    }

    public override GameObject FindPossibleAttackTargetInRange() {
        List<GameObject> possibleTargets = FindTargetsInRange(new List<string> { "Enemy", "EnemyMage"});
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }

}
