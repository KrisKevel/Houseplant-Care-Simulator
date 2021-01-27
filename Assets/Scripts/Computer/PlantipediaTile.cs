using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantipediaTile : MonoBehaviour
{
    public HouseplantData Plant;

    private Image _plantImage;
    private TextMeshProUGUI _plantName;

    private void Awake()
    {
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        _plantName = texts[0];
        _plantImage = gameObject.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        _plantName.text = Plant.HouseplantName;
        _plantImage.sprite = Plant.HouseplantPicture;
    }

    public void ShowInfo()
    {
        Events.BringUpPlantInfo(Plant);
    }
}
