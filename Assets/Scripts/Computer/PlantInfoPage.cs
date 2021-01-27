using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInfoPage : MonoBehaviour
{
    public Image PlantImage;

    private TextMeshProUGUI _name;
    private TextMeshProUGUI _generalInfo;
    private TextMeshProUGUI _waterRequirement;
    private TextMeshProUGUI _lightRequirement;

    void Awake()
    {
        Events.OnBringUpPlantInfo += OpenInfo;
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        _name = texts[0];
        _generalInfo = texts[1];
        _waterRequirement = texts[2];
        _lightRequirement = texts[3];
        gameObject.SetActive(false);
    }

    void OpenInfo(HouseplantData data)
    {
        gameObject.SetActive(true);
        PlantImage.sprite = data.HouseplantPicture;
        _name.text = "Name: " + data.HouseplantName;
        _generalInfo.text = "General: " + data.GeneralCareInfo;
        _waterRequirement.text = "Watering: " + data.WaterRequirementText;
        _lightRequirement.text = "Light: " + data.LightRequirementText;
    }

    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }
}
