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

    private void Awake()
    {
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        _plantName = texts[0];
        _plantPrice = texts[1];
    }

    private void Start()
    {
        _plantName.text = Plant.HouseplantName;
        _plantPrice.text = Plant.Price.ToString();
        PlantImage.sprite = Plant.HouseplantPicture;
    }

    public void Buy()
    {
        if (GameManager.Instance.GetFunds() < Plant.Price)
        {
            Events.InsufficientFunds();
        }
        else
        {
            GameManager.Instance.UpdateFunds(-Plant.Price);
            Events.BuyPlant(Plant.HouseplantPrefab);
        }
    }
}
