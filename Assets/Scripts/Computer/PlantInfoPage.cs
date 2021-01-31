using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInfoPage : MonoBehaviour
{
    public Image PlantImage;

    private TextMeshProUGUI _name;
    private TextMeshProUGUI _info;

    void Awake()
    {
        Events.OnBringUpPlantInfo += OpenInfo;
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        _name = texts[0];
        _info = texts[1];
        gameObject.SetActive(false);
    }

    void OpenInfo(HouseplantData data)
    {
        gameObject.SetActive(true);
        PlantImage.sprite = data.HouseplantPicture;
        _name.text = "Name: " + data.HouseplantName;
        _info.text = "General: " + data.GeneralCareInfo + "\n\n" +
            "Watering: " + data.WaterRequirementText + "\n\n" +
            "Light: " + data.LightRequirementText;
    }

    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }
}
