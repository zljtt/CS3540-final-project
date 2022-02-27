using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeUnitBehavior : MonoBehaviour {
    
    public Slider healthSlider1;
    public Slider healthSlider2;
    public int startHealth = 100;
    private int currentHealth;
    // Start is called before the first frame update
    private void Awake() {

        healthSlider1.maxValue = startHealth;
        healthSlider2.maxValue = startHealth;
    }
    void Start()
    {
        currentHealth = startHealth;
        healthSlider1.value = currentHealth;
        healthSlider2.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.name + " current health: " + currentHealth);
    }

    public void TakeDamage(int damageAmount) {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
            healthSlider1.value = currentHealth;
            healthSlider2.value = currentHealth;
        }
        if(currentHealth <= 0) {
            UnitDies();
        }
    }

    public bool checkDeath(int damageAmount) {
        return currentHealth - damageAmount <= 0;
    }

    void UnitDies() {
        Destroy(gameObject);
    }
}
