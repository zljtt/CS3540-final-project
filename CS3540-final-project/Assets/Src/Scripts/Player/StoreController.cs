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
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
            {
                FindObjectOfType<TooltipManager>().tooltip.SetActive(false);
                TooltipManager.hover = ItemDatabase.EMPTY;
                gui.SetActive(false);
                storeOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                gui.SetActive(true);
                storeOpen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
