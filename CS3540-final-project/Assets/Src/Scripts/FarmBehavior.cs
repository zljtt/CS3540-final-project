using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmBehavior : MonoBehaviour {
    
    public int startHealth = 100;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
