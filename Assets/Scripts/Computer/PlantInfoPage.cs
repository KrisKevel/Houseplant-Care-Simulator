using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInfoPage : MonoBehaviour
{
    public Image PlantImage;
    public TextMeshProUGUI ContentField;
    public LayoutElement WaterLayoutElement;
    public LayoutElement LightLayoutElement;
    public int CharacterWrapLimit;

    private TextMeshProUGUI _name;
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
        _wateringName = texts[5];
        _wateringInfo = texts[4];
        _lightName = texts[8];
        _lightinfo = texts[7];
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
        SetLayout();
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
            _wateringName.text = "[ Dry ]";
            _wateringName.color = Color.red;
        }
        else if (moisture < 66.6)
        {
            _wateringName.text = "[ Moist ]";
            _wateringName.color = Color.green;
        }
        else
        {
            _wateringName.text = "[ Wet ]";
            _wateringName.color = Color.blue;
        }
    }

    private void SetLightColor(float light)
    {
        if (light < 33.3)
        {
            _lightName.text = "[ Shadow ]";
            _lightName.color = Color.grey;
        }
        else if (light < 66.6)
        {
            _lightName.text = "[ Light ]";
            _lightName.color = new Color(0.9f, 0.82f, 0.6f, 1);
        }
        else
        {
            _lightName.text = "[ Sunny ]";
            _lightName.color = Color.yellow;
        }
    }

    private void SetLayout()
    {
        int waterContentLenght = _wateringInfo.text.Length;
        int lightContentLenght = _lightinfo.text.Length;

        LightLayoutElement.enabled = lightContentLenght > CharacterWrapLimit ? true : false;
        WaterLayoutElement.enabled = waterContentLenght > CharacterWrapLimit ? true : false;
    }
}
