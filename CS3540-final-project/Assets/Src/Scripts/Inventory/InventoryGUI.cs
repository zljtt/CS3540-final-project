using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private List<Transform> itemSlots; //sprites

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
        Render();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    private void Render()
    {
        int x = 0;
        int y = 0;
        float itemSlotSizeOffset = 55f;
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Image image = itemSlots[x].Find("ItemImage").GetComponent<Image>();
            Texture texture = Resources.Load("Art/Textures/Items/" + itemStack.GetItem().GetRegistryName() + ".png") as Texture2D;
            image.material.mainTexture = texture;
        }
    }

    private void RefreshInventoryItems()
    {

        foreach (ItemStack itemStack in inventory.GetItemList())
        {
            /*
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotSizeOffset, y * itemSlotSizeOffset);
            */

            x++;
        }
    }


}
