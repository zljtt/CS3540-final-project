using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour
{
    public float moveSpeed;
    private CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        controller.Move(new Vector3(horizontalMovement * moveSpeed * Time.deltaTime, 0, verticalMovement * moveSpeed * Time.deltaTime));
    }
}
