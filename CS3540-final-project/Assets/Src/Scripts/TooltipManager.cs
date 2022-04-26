using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltip;
    private Text tooltipTitle;
    private Text tooltipContent;

    public static Item hover = ItemDatabase.EMPTY;
    void Start()
    {
        tooltipTitle = tooltip.transform.Find("Title").GetComponent<Text>();
        tooltipContent = tooltip.transform.Find("Content").GetComponent<Text>();
    }
    void Update()
    {
        if (hover != ItemDatabase.EMPTY)
        {
            tooltip.SetActive(true);
            tooltip.transform.position = Input.mousePosition + new Vector3(300, -200, 0);
            tooltipTitle.text = hover.GetProperties().GetName();
            tooltipContent.text = hover.GetProperties().GetDescription();
        }
        else
        {
            tooltip.SetActive(false);
        }
    }
}
