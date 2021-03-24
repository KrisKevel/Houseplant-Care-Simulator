using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Confirmation : MonoBehaviour
{
    public TextMeshProUGUI Days;
    public TextMeshProUGUI PlantName;

    private void Awake()
    {
        Events.OnBuyPlant += Open;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnBuyPlant -= Open;
    }

    public void Open(GameObject houseplant)
    {
        HouseplantData houseplentData = houseplant.gameObject.GetComponent<HouseplantHealth>().Houseplant;
        Days.text = houseplentData.DaysForDelivery.ToString();
        PlantName.text = houseplentData.HouseplantName;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
