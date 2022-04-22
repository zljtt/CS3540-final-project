using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGui : MonoBehaviour
{
    private Transform[] slots;
    // Start is called before the first frame update
    void Awake()
    {
        slots = new Transform[Store.SIZE];
        for (int i = 0; i < Store.SIZE; i++)
        {
            slots[i] = transform.Find("Slot" + (i + 1));
        }
    }

    void Update()
    {
        Render();
    }

    private void Render()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Image image = slots[i].Find("ItemImage").GetComponent<Image>();
            Text amountText = slots[i].Find("AmountText").GetComponent<Text>();
            Text valueText = slots[i].Find("Value").GetComponent<Text>();
            if (StoreManager.store.GetEntryAt(i) == Store.EMPTY_ENTRY)
            {
                image.gameObject.SetActive(false);
                amountText.gameObject.SetActive(false);
                valueText.gameObject.SetActive(false);
            }
            else
            {
                ItemStack itemStack = StoreManager.store.GetEntryAt(i).itemStack;
                if (itemStack.GetAmount() > 1)
                {
                    amountText.text = itemStack.GetAmount().ToString();
                    amountText.gameObject.SetActive(true);
                }

                image.gameObject.SetActive(true);
                image.sprite = itemStack.GetItem().GetSprite();
                valueText.gameObject.SetActive(true);
                valueText.text = StoreManager.store.GetEntryAt(i).value.ToString();
            }
        }
    }
}
