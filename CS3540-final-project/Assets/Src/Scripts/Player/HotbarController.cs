using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/* PlayerInventory
 * Handles using and picking up item
 */
public class HotbarController : MonoBehaviour
{
    public HotbarGui gui;
    public Transform playerCamera;

    void Update()
    {
        DetectKeyPress();
        DetectKeyRelease();
    }


    private void DetectKeyPress()
    {
        for (int keyToCheck = 1; keyToCheck <= 9; keyToCheck++)
        {
            if (Input.GetKeyDown(keyToCheck.ToString()))
            {
                gui.KeyPressing(keyToCheck - 1);
            }
        }
    }

    // detect key release and use item
    private void DetectKeyRelease()
    {
        for (int keyToCheck = 1; keyToCheck <= 9; keyToCheck++)
        {
            if (Input.GetKeyUp(keyToCheck.ToString()) && Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, Mathf.Infinity))
            {
                ItemStack itemStack = LevelManager.inventory.GetItemAt(keyToCheck - 1);

                gui.KeyReleased(keyToCheck - 1, itemStack.Use(playerCamera, hit, keyToCheck - 1));
            }
        }
    }


}
