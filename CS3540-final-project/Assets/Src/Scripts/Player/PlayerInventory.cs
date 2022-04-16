using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* PlayerInventory
 * Handles using and picking up item
 */
public class PlayerInventory : MonoBehaviour
{
    public InventoryGUI gui;
    public static Inventory inventory;
    void Awake()
    {
        inventory = new Inventory();
        inventory.ReadData();
    }
    void OnDestroy()
    {
        inventory.WriteData();
    }

    void Update()
    {
        DetectKeyPress();
        DetectKeyRelease();
        // update cooldown
        foreach (ItemStack itemStack in inventory.GetItemList())
        {
            itemStack.UpdateCooldown(Time.deltaTime);
        }
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
        if (Input.GetKeyDown("0"))
        {
            gui.KeyPressing(9);
        }
    }

    // detect key release and use item
    private void DetectKeyRelease()
    {
        RaycastHit hit;
        for (int keyToCheck = 1; keyToCheck <= 9; keyToCheck++)
        {
            if (Input.GetKeyUp(keyToCheck.ToString()) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                gui.KeyReleased(keyToCheck - 1, inventory.UseItemAtIndex(keyToCheck - 1, transform, hit));
            }
        }
        if (Input.GetKeyUp("0") && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(9, inventory.UseItemAtIndex(9, transform, hit));
        }
    }
}
