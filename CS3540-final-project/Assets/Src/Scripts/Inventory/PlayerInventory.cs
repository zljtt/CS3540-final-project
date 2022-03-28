using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* PlayerInventory
 * Handles using and picking up item
 */
public class PlayerInventory : MonoBehaviour
{
    public InventoryGUI gui;
    private Inventory inventory;
    void Start()
    {
        inventory = new Inventory();
        gui.SetInventory(inventory);
        inventory.AddItem(ItemDatabase.HEALTH_POTION, 1);
    }

    void Update()
    {

    }

    private void DetectKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            gui.KeyPressing(0);
        }
    }

    // detect key release and use item
    private void DetectKeyRelease()
    {
        RaycastHit hit;
        if (Input.GetKeyUp(KeyCode.Keypad1) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(0, inventory.GetItemList()[0].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad2) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(1, inventory.GetItemList()[1].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad3) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(2, inventory.GetItemList()[2].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad4) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(3, inventory.GetItemList()[3].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad5) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(4, inventory.GetItemList()[4].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad6) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(5, inventory.GetItemList()[5].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad7) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(6, inventory.GetItemList()[6].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad8) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(7, inventory.GetItemList()[7].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad9) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(8, inventory.GetItemList()[8].UseItem(transform, hit.transform));
        }
        if (Input.GetKeyUp(KeyCode.Keypad0) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(9, inventory.GetItemList()[9].UseItem(transform, hit.transform));
        }
    }
}
