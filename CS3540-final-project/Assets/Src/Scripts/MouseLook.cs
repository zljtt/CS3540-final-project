using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitiviy = 100f;
    float pitch = 0;
    //float yaw = 0;
    Transform playerBody;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent.transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitiviy * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitiviy * Time.deltaTime * 0.8f;
        // Debug.Log(moveY);

        // yaw
        // yaw += moveX;
        playerBody.Rotate(Vector3.up * moveX);

        // pitch
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -75, 75);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);

    }
}
