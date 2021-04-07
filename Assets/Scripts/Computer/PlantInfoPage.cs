using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInfoPage : MonoBehaviour
{
    public Image PlantImage;

    private TextMeshProUGUI _name;
    private TextMeshProUGUI _generalName;
    private TextMeshProUGUI _generalInfo;
    private TextMeshProUGUI _wateringName;
    private TextMeshProUGUI _wateringInfo;
    private TextMeshProUGUI _lightName;
    private TextMeshProUGUI _lightinfo;

    void Awake()
    {
        Events.OnBringUpPlantInfo += OpenInfo;
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        _name = texts[0];
        _generalInfo = texts[2];
        _wateringName = texts[3];
        _wateringInfo = texts[4];
        _lightName = texts[5];
        _lightinfo = texts[6];
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Events.OnBringUpPlantInfo -= OpenInfo;
    }

    void OpenInfo(HouseplantData data)
    {
        LoadInfo(data);
        UpdateColors(data);
        gameObject.SetActive(true);
    }

    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }

    private void LoadInfo(HouseplantData data)
    {
        PlantImage.sprite = data.HouseplantPicture;
        _name.text = "Name: " + data.HouseplantName;
        _generalInfo.text = data.GeneralCareInfo;
        _wateringInfo.text = data.WaterRequirementText;
        _lightinfo.text = data.LightRequirementText;

    }

    private void UpdateColors(HouseplantData data)
    {
        SetLightColor(data.LightRequirement);
        SetWaterColor(data.WaterRequirement);
    }

    private void SetWaterColor(float moisture)
    {
        if (moisture < 33.3)
        {
            _wateringName.color = Color.red;
        }
        else if (moisture < 66.6)
        {
            _wateringName.color = Color.green;
        }
        else
        {
            _wateringName.color = Color.blue;
        }
    }

    private void SetLightColor(float light)
    {
        if (light < 33.3)
        {
            _lightName.color = Color.grey;
        }
        else if (light < 66.6)
        {
            _lightName.color = new Color(0.9f, 0.82f, 0.6f, 1);
        }
        else
        {
            _lightName.color = Color.yellow;
        }
    }
}
