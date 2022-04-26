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
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
            {
                FindObjectOfType<TooltipManager>().tooltip.SetActive(false);
                TooltipManager.hover = ItemDatabase.EMPTY;
                gui.SetActive(false);
                inventoryOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            {
                gui.SetActive(true);
                inventoryOpen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }


}
