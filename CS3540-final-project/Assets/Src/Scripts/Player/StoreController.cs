using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public GameObject gui;
    public static bool storeOpen;

    void Update()
    {
        if (storeOpen)
        {
            if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Escape))
            {
                gui.SetActive(false);
                storeOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                gui.SetActive(true);
                storeOpen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
