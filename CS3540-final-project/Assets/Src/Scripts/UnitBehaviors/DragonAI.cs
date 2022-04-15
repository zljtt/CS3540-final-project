using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonAI : UnitBehavior
{
    public enum DragonState { IDLE, RISE, ALERT, CHASE, ATTACK, DIE}
    DragonState currentDragonState;
    float currentHeight = 0;

    protected override void Start()
    {
        healthSliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.maxValue = maxHealth;
        }
        //currentDragonState = DragonState.IDLE;
    }

    public override void Attack(GameObject target) {

    }

    private void FixedUpdate() {
        print(currentDragonState);
    }

    public override GameObject FindPossibleAttackTargetInRange() {
        return this.gameObject;
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

    }

        // when an unit does not have a target, it enters alert state and wait for target
    protected override void PerformAlert() {

    }

    // when an unit has a target, it will chase the target until entering attack range
    protected override void PerformChase() {

    }

    // when an unit is within attack range, it attack until the target dies
    protected override void PerformAttack() {

    }

    // when an unit die
    protected override void PerformDie() {

    }

    public void ChangeState(DragonState state) {
        currentDragonState = state;
    }

}
