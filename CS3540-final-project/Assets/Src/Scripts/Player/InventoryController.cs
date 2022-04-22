using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject gui;
    public static bool inventoryOpen;
    void Update()
    {
        if (inventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                gui.SetActive(false);
                inventoryOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                gui.SetActive(true);
                inventoryOpen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }


}
