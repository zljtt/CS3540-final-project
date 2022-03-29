using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActivation : MonoBehaviour
{
    public GameObject healVFX;
    // Start is called before the first frame update
    private void Awake() {
        healVFX.SetActive(false);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activateHealEffect() {
        healVFX.SetActive(true);
    }
}
