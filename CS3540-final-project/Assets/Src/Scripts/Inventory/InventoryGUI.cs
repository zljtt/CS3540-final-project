using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    public int slotCount = 10;
    private Inventory inventory;
    private Transform itemSlotContainer;
    private List<Transform> itemSlots = new List<Transform>();
    // States of keys. 0 = not pressed, 1 = being pressed, 2 = released, succeed to use, 3 = released, fail to use
    private List<int> keyStates = new List<int>();

    private Color originalColor;

    void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        if (itemSlotContainer)
        {
            print("found slot containter");
        }
        for (int i = 1; i <= slotCount; i++)
        {
            itemSlots.Add(itemSlotContainer.Find("ItemSlot" + i));
            print("found slot" + i);
            keyStates.Add(0);
            print("key state added, number of keys: " + keyStates.Count);
        }
        originalColor = itemSlots[0].Find("Border").GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    private void Render()
    {
        int x = 0;
        // int y = 0;
        // float itemSlotSizeOffset = 55f;
        for (x = 0; x < slotCount; x++)
        {
            Image image = itemSlots[x].Find("ItemImage").GetComponent<Image>();
            Text amountText = itemSlots[x].Find("AmountText").GetComponent<Text>();
            Image borderHighlight = itemSlotContainer.Find("BorderHighlight" + (x + 1)).GetComponent<Image>();
            if (x >= inventory.GetItemList().Count || inventory.GetItemList()[x].GetAmount() == 0)
            {
                image.gameObject.SetActive(false);
                amountText.gameObject.SetActive(false);
            }
            else
            {
                image.sprite = inventory.GetItemList()[x].GetItem().GetSprite();
                amountText.text = inventory.GetItemList()[x].GetAmount().ToString();
                image.gameObject.SetActive(true);
                amountText.gameObject.SetActive(true);
            }
            switch (keyStates[x])
            {
                case 1:
                    borderHighlight.color = Color.blue;
                    borderHighlight.gameObject.SetActive(true);
                    break;
                case 2:
                    borderHighlight.color = Color.green;
                    borderHighlight.gameObject.SetActive(true);
                    break;
                case 3:
                    borderHighlight.color = Color.red;
                    borderHighlight.gameObject.SetActive(true);
                    break;
                default:
                    borderHighlight.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void KeyPressing(int keyIndex)
    {
        if (keyStates[keyIndex] == 0)
        {
            keyStates[keyIndex] = 1;
        }
    }

    public void KeyReleased(int keyIndex, bool isUsed)
    {
        if (keyStates[keyIndex] == 1)
        {
            if (isUsed)
            {
                keyStates[keyIndex] = 2;
            }
            else
            {
                keyStates[keyIndex] = 3;
            }
            // TODO: RESET KEY STATE AFTER A SHORT PAUSE maybe use coroutine?
            
        }
    }
}
