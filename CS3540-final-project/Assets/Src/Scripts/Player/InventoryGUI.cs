using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGui : MonoBehaviour
{
    private Transform[] itemSlots = new Transform[36];
    private int selectedIndex = -1;

    private Color normal = new Color(0xE7, 0xE7, 0xE7, 0xAF);
    private Color selected = new Color(0xEF, 0xEF, 0xEF, 0xEF);
    void Awake()
    {

        for (int i = 1; i < 10; i++)
        {
            itemSlots[i - 1] = transform.Find("Hotbar0" + i);
        }
        for (int i = 10; i < 37; i++)
        {
            itemSlots[i - 1] = transform.Find("Slot" + i);
        }

        normal = itemSlots[0].GetComponent<Button>().colors.normalColor;
        selected = itemSlots[0].GetComponent<Button>().colors.highlightedColor;

    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    private void Render()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Image image = itemSlots[i].Find("ItemImage").GetComponent<Image>();
            Text amountText = itemSlots[i].Find("AmountText").GetComponent<Text>();
            if (LevelManager.inventory.GetItemAt(i) == Inventory.EMPTY)
            {
                image.gameObject.SetActive(false);
                amountText.gameObject.SetActive(false);
            }
            else
            {
                ItemStack itemStack = LevelManager.inventory.GetItemList()[i];
                if (itemStack.GetAmount() > 1)
                {
                    amountText.text = itemStack.GetAmount().ToString();
                    amountText.gameObject.SetActive(true);
                }

                image.gameObject.SetActive(true);
                image.sprite = itemStack.GetItem().GetSprite();
                if (itemSlots[i].Find("CooldownSlider") != null)
                {
                    Slider cooldownSlider = itemSlots[i].Find("CooldownSlider").GetComponent<Slider>();
                    // Checking cooldown
                    if (itemStack.GetCurrentCooldown() <= 0)
                    {
                        // Disable slider if item is ready to use
                        cooldownSlider.gameObject.SetActive(false);
                    }
                    else
                    {
                        // Update slider if not
                        cooldownSlider.gameObject.SetActive(true);
                        cooldownSlider.maxValue = itemStack.GetMaxCooldown();
                        cooldownSlider.value = itemStack.GetMaxCooldown() - itemStack.GetCurrentCooldown();
                    }
                }

            }
        }
    }

    public void Select(GameObject slot)
    {
        int index = int.Parse(slot.name.Substring(slot.name.Length - 2)) - 1;
        if (index >= 0 && index < Inventory.SIZE)
        {
            if (selectedIndex == -1)
            {

                if (LevelManager.inventory.GetItemAt(index) != Inventory.EMPTY)
                {
                    selectedIndex = index;

                    ColorBlock cb = itemSlots[selectedIndex].GetComponent<Button>().colors;
                    cb.normalColor = selected;
                    cb.selectedColor = selected;
                    itemSlots[selectedIndex].GetComponent<Button>().colors = cb;
                }
            }
            else
            {
                ColorBlock cb1 = itemSlots[selectedIndex].GetComponent<Button>().colors;
                cb1.normalColor = normal;
                cb1.selectedColor = normal;
                itemSlots[selectedIndex].GetComponent<Button>().colors = cb1;
                ColorBlock cb2 = itemSlots[index].GetComponent<Button>().colors;
                cb2.normalColor = normal;
                cb2.selectedColor = normal;
                itemSlots[index].GetComponent<Button>().colors = cb2;

                if (selectedIndex != index)
                {
                    LevelManager.inventory.Swap(index, selectedIndex);
                }
                selectedIndex = -1;
            }
        }
    }
}
