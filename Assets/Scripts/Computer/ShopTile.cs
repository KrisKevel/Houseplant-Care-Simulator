using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTile : MonoBehaviour
{
    public HouseplantData Plant;
    public Image PlantImage;

    private TextMeshProUGUI _plantName; 
    private TextMeshProUGUI _plantPrice;
    private TextMeshProUGUI _plantDeliveryDays;

    private void Awake()
    {
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        _plantName = texts[0];
        _plantPrice = texts[1];
        _plantDeliveryDays = texts[2];
    }

    private void Start()
    {
        _plantName.text = Plant.HouseplantName;
        _plantPrice.text = Plant.Price.ToString();
        _plantDeliveryDays.text = Plant.DaysForDelivery.ToString();
        PlantImage.sprite = Plant.HouseplantPicture;
    }

    public void Buy()
    {
        if (GameManager.Instance.GetFunds() < Plant.Price)
        {
            Events.InsufficientFunds();
        }
        else if (GameManager.Instance.CurrentState != GameManager.GameState.tutorial)
        {
            GameManager.Instance.UpdateFunds(-Plant.Price);
            Events.BuyPlant(Plant.HouseplantPrefab);
        }
    }
}
