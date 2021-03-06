using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarGui : MonoBehaviour
{
    public int slotCount = 9;
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
            //print("found slot containter");
        }
        for (int i = 1; i <= slotCount; i++)
        {
            itemSlots.Add(itemSlotContainer.Find("ItemSlot" + i));
            print("found slot" + i);
            keyStates.Add(0);
            //print("key state added, number of keys: " + keyStates.Count);
        }
        originalColor = itemSlots[0].Find("Border").GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        Render();
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
            // Checking to see if the item exists
            if (x >= Inventory.SIZE || LevelManager.inventory.GetItemAt(x).GetAmount() == 0)
            {
                // Disable UI element if doesn't exist
                image.gameObject.SetActive(false);
                amountText.gameObject.SetActive(false);
            }
            else
            {
                // Update UI element if exists
                ItemStack itemStackX = LevelManager.inventory.GetItemList()[x];
                image.sprite = itemStackX.GetItem().GetSprite();
                image.gameObject.SetActive(true);
                amountText.text = itemStackX.GetAmount().ToString();
                amountText.gameObject.SetActive(itemStackX.GetAmount() > 1);
                Slider cooldownSlider = itemSlots[x].Find("CooldownSlider").GetComponent<Slider>();
                // Checking cooldown
                if (itemStackX.GetCurrentCooldown() <= 0)
                {
                    // Disable slider if item is ready to use
                    cooldownSlider.gameObject.SetActive(false);
                }
                else
                {
                    // Update slider if not
                    cooldownSlider.gameObject.SetActive(true);
                    cooldownSlider.maxValue = itemStackX.GetMaxCooldown();
                    cooldownSlider.value = itemStackX.GetMaxCooldown() - itemStackX.GetCurrentCooldown();
                }
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
            StartCoroutine("ResetKeyState", keyIndex);
        }
    }

    private IEnumerator ResetKeyState(int keyIndex)
    {
        yield return new WaitForSeconds(0.5f);
        keyStates[keyIndex] = 0;
    }
}
