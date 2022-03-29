using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActivation : MonoBehaviour
{
    public GameObject healVFX;
    // Start is called before the first frame update
    void Start()
    {
        healVFX.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activateHealEffect() {
        healVFX.SetActive(true);
    }
}
