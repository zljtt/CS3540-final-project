using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DragonAI : AllyBehavior
{
    public Transform shootPoint;

    float currentHeight = -0.5f;
    public float destinyHeight = 3;
    Vector3 currentPosition;
    public AudioClip attackSFX;
    public GameObject firePrefab;

    protected override void Start()
    {
        base.Start();
        currentPosition = this.gameObject.transform.position;
        currentPosition.y = currentHeight;
        this.gameObject.transform.position = currentPosition;
        this.GetComponent<NavMeshAgent>().enabled = false;
    }


    protected override void Update()
    {
        base.Update();
        currentPosition = this.gameObject.transform.position;
        currentHeight = currentPosition.y;
        if (currentAttackTarget != null)
        {
            FaceTarget(currentAttackTarget.transform.position);
        }
    }

    protected override void PerformIdle()
    {
        if (currentHeight <= 0)
        {
            anim.SetInteger("animState", OTHER_ANIM);
        }
        else
        {
            anim.SetInteger("animState", IDLE_ANIM);
        }
        if (currentHeight < destinyHeight)
        {
            Vector3 newPos = new Vector3(currentPosition.x, currentPosition.y + 0.025f, currentPosition.z);
            this.gameObject.transform.position = newPos;
        }
    }


    // when an unit has a target, it will chase the target until entering attack range
    protected override void PerformChase()
    {
        anim.SetInteger("animState", WALK_ANIM);
        if (currentAttackTarget == null)
        {
            currentState = State.ALERT;
        }
        else if (CanReach(currentAttackTarget))
        {
            currentState = State.ATTACK;
        }
        else
        {
            Vector3 targetPosition = currentAttackTarget.transform.position;
            targetPosition.y = currentHeight;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    public override void Attack(GameObject target)
    {
        anim.SetTrigger(ATTACK1_TRIGGER);
        Invoke("Shoot", 0.4f);
    }

    public void Shoot()
    {
        if (currentAttackTarget != null)
        {
            AudioSource.PlayClipAtPoint(attackSFX, playerPosition.position);
            Vector3 direction = currentAttackTarget.transform.position - gameObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject projectile = Instantiate(firePrefab, shootPoint.position, rotation);
            var behavior = projectile.GetComponent<ProjectileBehavior>();
            behavior.attackDamage = attackDamage;
            behavior.shooter = gameObject;
        }

    }
    // when an unit die
    protected override void PerformDie()
    {
        anim.SetInteger("animState", DIE_ANIM);
        Destroy(gameObject, 2);
    }

    public override GameObject FindPossibleAttackTargetInRange()
    {
        List<GameObject> possibleTargets = FindTargetsInRange(new string[] { "Enemy" });
        GameObject closest = FindClosest(transform, possibleTargets);
        return closest;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
    public override bool TargetInSight()
    {
        return true;
    }

}
