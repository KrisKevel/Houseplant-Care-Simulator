using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantipediaTile : MonoBehaviour
{
    public HouseplantData Plant;
    public Image PlantImage;

    private TextMeshProUGUI _plantName;

    private void Awake()
    {
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        _plantName = texts[0];
    }

    private void Start()
    {
        _plantName.text = Plant.HouseplantName;
        PlantImage.sprite = Plant.HouseplantPicture;
    }

    public void ShowInfo()
    {
        Events.BringUpPlantInfo(Plant);
    }
}
