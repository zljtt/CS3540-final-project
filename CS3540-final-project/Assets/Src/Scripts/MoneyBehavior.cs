using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBehavior : MonoBehaviour
{
    public int amount = 5;
    public AudioClip coinSFX;


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.LookAt(player.transform);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.playerData.GainMoney(amount);
            AudioSource.PlayClipAtPoint(coinSFX, transform.position);
            Destroy(gameObject);
        }
    }
}
