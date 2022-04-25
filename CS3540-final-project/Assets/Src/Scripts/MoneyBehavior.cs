using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBehavior : MonoBehaviour
{
    public int amount = 5;
    public AudioClip coinSFX;

    private float lowY;
    private bool isFalling;
    private float fallVelocity;
    void Start()
    {
        isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        if (!isFalling)
        {
            float offset = Mathf.PingPong(Time.time / 10, 0.2f);
            Vector3 newPos = transform.position;
            newPos.y = lowY + offset;
            transform.position = newPos;
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
        else if (isFalling && other.gameObject.isStatic)
        {
            isFalling = false;
            lowY = transform.position.y;
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }
}
