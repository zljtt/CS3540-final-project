using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : AllyBehavior
{
    public enum DragonState { IDLE, RISE, ALERT, CHASE, ATTACK, DIE}
    DragonState currentDragonState;
    float currentHeight = 0;
    private Slider[] healthSliders;

    protected override void Start()
    {
        healthSliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (var healthSlider in healthSliders)
        {
            healthSlider.maxValue = maxHealth;
        }
    }

    public override void Attack(GameObject target) {

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

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
