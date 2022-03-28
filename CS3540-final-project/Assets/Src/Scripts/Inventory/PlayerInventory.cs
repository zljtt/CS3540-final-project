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
    }

    void Update()
    {

    }
}
