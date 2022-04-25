using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        int index = int.Parse(gameObject.name.Substring(gameObject.name.Length - 2, 2)) - 1;
        TooltipManager.hover = LevelManager.inventory.GetItemAt(index).GetItem();
    }

    public void OnPointerExit(PointerEventData data)
    {
        TooltipManager.hover = ItemDatabase.EMPTY;
    }
}
