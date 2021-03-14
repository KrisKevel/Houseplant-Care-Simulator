using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem current;
    public Tooltip tooltip;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        print("TEXT SET");
        current.tooltip.gameObject.SetActive(true);
        print("TOOLTIP ON");
    }
    public static void Hide()
    {
        if (current.tooltip != null)
        {
            current.tooltip.gameObject.SetActive(false);
        }
    }
}
