using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGui : MonoBehaviour
{
    private Transform[] slots;
    private Transform purchase;
    private Transform money;

    private Color normal;
    private Color wrong;
    private int selectedIndex = -1;
    // Start is called before the first frame update
    void Awake()
    {
        slots = new Transform[Store.SIZE];
        for (int i = 0; i < Store.SIZE; i++)
        {
            slots[i] = transform.Find("Slot" + (i + 1));
        }
        money = transform.Find("Money");
        purchase = transform.Find("ConfirmBuy");
        normal = purchase.Find("Text").GetComponent<Text>().color;
        ColorUtility.TryParseHtmlString("#DB2625", out wrong);
    }

    void Update()
    {
        Render();
    }

    private void Render()
    {
        money.GetComponent<Text>().text = "COIN: $" + LevelManager.playerData.money;
        if (selectedIndex != -1 && StoreManager.store.GetEntryAt(selectedIndex) != Store.EMPTY_ENTRY)
        {
            purchase.Find("Text").GetComponent<Text>().text = "Purchase ($" + StoreManager.store.GetEntryAt(selectedIndex).value + ")";
        }
        else
        {
            purchase.Find("Text").GetComponent<Text>().text = "Purchase";
        }
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
                valueText.text = "$ " + StoreManager.store.GetEntryAt(i).value.ToString();
            }
        }
    }

    public void Select(GameObject slot)
    {
        int index = int.Parse(slot.name.Substring(slot.name.Length - 1)) - 1;
        selectedIndex = index;
    }

    public void Purchase()
    {
        if (selectedIndex < 0 || selectedIndex >= Store.SIZE || StoreManager.store.GetEntryAt(selectedIndex) == Store.EMPTY_ENTRY || !StoreManager.store.Purchase(selectedIndex))
        {
            purchase.Find("Text").GetComponent<Text>().color = wrong;
            selectedIndex = -1;
            Invoke("resetColor", 0.2f);
        }
    }

    void resetColor()
    {
        purchase.Find("Text").GetComponent<Text>().color = normal;
    }
}
