using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryPanel : MonoBehaviour
{
    private TextMeshProUGUI _plantsInDelivery;
    private TooltipTrigger _tooltip;
    
    private void Awake()
    {
        Events.OnDeliveryUpdate += UpdateDeliveryStats;

    }

    void Start()
    {
        _plantsInDelivery = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _tooltip = gameObject.GetComponent<TooltipTrigger>();
    }

    private void OnDestroy()
    {
        Events.OnDeliveryUpdate -= UpdateDeliveryStats;
    }

    private void UpdateDeliveryStats(List<KeyVal<HouseplantData, int>> toBeDelivered)
    {
        _plantsInDelivery.text = toBeDelivered.Count.ToString();
        _tooltip.Content = GenerateTooltipContent(toBeDelivered);
    }

    private string GenerateTooltipContent(List<KeyVal<HouseplantData, int>> toBeDelivered)
    {
        if (toBeDelivered.Count == 0)
        {
            return "No plants are in delivery";
        }

        string result = "";

        foreach (KeyVal<HouseplantData, int> pair in toBeDelivered)
        {
            result += pair.key.HouseplantName + " will arrive in " + pair.value + " days\n";
        }

        return result;
    }
}
