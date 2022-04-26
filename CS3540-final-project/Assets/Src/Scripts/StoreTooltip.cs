using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class StoreTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        int index = int.Parse(gameObject.transform.parent.gameObject.name.Substring(gameObject.transform.parent.gameObject.name.Length - 1, 1)) - 1;
        TooltipManager.hover = StoreManager.store.entries[index].itemStack.GetItem();
    }

    public void OnPointerExit(PointerEventData data)
    {
        TooltipManager.hover = ItemDatabase.EMPTY;
    }
}
