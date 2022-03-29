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
        inventory.AddItem(ItemDatabase.FARMER_SPAWNER, 100);
    }

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
        if (Input.GetKeyDown("0"))
        {
            gui.KeyPressing(9);
        }
        /*
        if (Input.GetKeyDown("1"))
        {
            print("1 is pressed!");
            gui.KeyPressing(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            print("2 is pressed!");
            gui.KeyPressing(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            print("3 is pressed!");
            gui.KeyPressing(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            print("4 is pressed!");
            gui.KeyPressing(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            print("5 is pressed!");
            gui.KeyPressing(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            print("6 is pressed!");
            gui.KeyPressing(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            print("7 is pressed!");
            gui.KeyPressing(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            print("8 is pressed!");
            gui.KeyPressing(7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            print("9 is pressed!");
            gui.KeyPressing(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            print("0 is pressed!");
            gui.KeyPressing(9);
        }
        */
    }

    // detect key release and use item
    private void DetectKeyRelease()
    {
        RaycastHit hit;
        for (int keyToCheck = 1; keyToCheck <= 9; keyToCheck++)
        {
            if (Input.GetKeyUp(keyToCheck.ToString()) && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                /*if (inventory.GetItemList().Count >= keyToCheck)
                {
                    doesUse = inventory.GetItemList()[keyToCheck - 1].UseItem(transform, hit.transform);
                }
                else
                {
                    doesUse = false;
                }*/
                gui.KeyReleased(keyToCheck - 1, inventory.UseItemAtIndex(keyToCheck - 1, transform, hit));
            }
        }
        if (Input.GetKeyUp("0") && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            gui.KeyReleased(9, inventory.UseItemAtIndex(9, transform, hit));
        }
        /*
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
        }*/
    }
}
