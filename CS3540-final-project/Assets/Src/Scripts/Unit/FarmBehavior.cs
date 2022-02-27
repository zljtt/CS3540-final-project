using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmBehavior : UnitBehavior
{
    private int startHealth = 100;
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
