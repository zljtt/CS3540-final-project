using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private List<Transform> itemSlots;

    async void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        for (int i = 1; i < 11; i++)
        {
            itemSlots.Add(itemSlotContainer.Find("ItemSlot" + i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotSizeOffset = 55f;
        foreach (Item item in inventory.GetItemList())
        {
            /*
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotSizeOffset, y * itemSlotSizeOffset);
            */
            Image image = itemSlots[x].Find("ItemImage").GetComponent<Image>();
            image.sprite = item.sprite;
            x++;
        }
    }
}
