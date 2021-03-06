﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoistureMeterPanel : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI MoistureLevel;
    public TextMeshProUGUI WaterStatus;
    public TextMeshProUGUI LightLevel;
    public TextMeshProUGUI LightStatus;
    public TextMeshProUGUI HouseplantName;
    public Image Health;

    private HouseplantHealth _houseplant;
    private bool _panelEnabled = false;

    private bool _waterButtonPressed = false;
    private bool _mouseIsOver = false;

    private float _defaultWaterAddedPerFrame = 0.02f;
    private float _waterAddedPerFrame = 0.02f;
    private float _waterAddedPerFrameAccel = 0.00001f;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        Events.OnOpenMoistureMeter += OpenMenu;
        Events.OnEnableMoistureMeter += EnablePanel;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_waterButtonPressed)
        {
            _houseplant.IncreaseWaterLevel(_waterAddedPerFrame);
            _waterAddedPerFrame += _waterAddedPerFrameAccel;
        }

        UpdateData();
        CheckDistanceToPlayer();
        CheckPlantHealth();
    }

    private void OnDestroy()
    {
        Events.OnOpenMoistureMeter -= OpenMenu;
        Events.OnEnableMoistureMeter -= EnablePanel;
    }

    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html?page=1&pageSize=5&sort=votes 
    //Ziplock9000's answer + BluishGreenPro's comment
    public void OnDeselect(BaseEventData eventData)
    {
        if (!_mouseIsOver)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html?page=1&pageSize=5&sort=votes 
    //Ziplock9000's answer + BluishGreenPro's comment


    public void OpenMenu(HouseplantHealth houseplant)
    {
        if (!_panelEnabled) { return; }
        EventSystem.current.SetSelectedGameObject(gameObject);

        _houseplant = houseplant;
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);
        GameManager.Instance.UIPanelUp = true;

        UpdateData();
    }


    private void UpdateData()
    {
        HouseplantName.text = _houseplant.Houseplant.HouseplantName;

        float moisture = _houseplant.GetWaterLevel();
        MoistureLevel.text = System.Math.Round(moisture, 1).ToString();
        SetWaterStatus(moisture);

        float light = _houseplant.GetLightLevel();
        LightLevel.text = System.Math.Round(light, 2).ToString();
        SetLightStatus(light);

        Health.fillAmount = _houseplant.Health / 100f;
    }

    private void SetWaterStatus(float moisture)
    {
        if(moisture < 33.3)
        {
            WaterStatus.text = "Dry";
            WaterStatus.color = Color.red;
        }
        else if(moisture < 66.6)
        {
            WaterStatus.text = "Moist";
            WaterStatus.color = Color.green;
        }
        else
        {
            WaterStatus.text = "Wet";
            WaterStatus.color = Color.blue;
        }
    }

    private void SetLightStatus(float light)
    {
        if (light < 33.3)
        {
            LightStatus.text = "Shadow";
            LightStatus.color = Color.grey;
        }
        else if (light < 66.6)
        {
            LightStatus.text = "Light";
            LightStatus.color = new Color(0.9f, 0.82f, 0.6f, 1);
        }
        else
        {
            LightStatus.text = "Sunny";
            LightStatus.color = Color.yellow;
        }
    }

    public void WaterButtonDown()
    {
        _waterButtonPressed = true;
    }

    public void WaterButtonUp()
    {
        _waterButtonPressed = false;
        _waterAddedPerFrame = _defaultWaterAddedPerFrame;
    }
    
    public void CheckDistanceToPlayer()
    {
        bool closeEnough = Vector3.Distance(
            GameObject.Find("Player").transform.position, 
            _houseplant.gameObject.transform.position
            ) < GameManager.Instance.AOE;

        if (!closeEnough)
        {
            gameObject.SetActive(false);
        }
    }

    public void CheckPlantHealth()
    {
        if (_houseplant.Dead)
        {
            gameObject.SetActive(false);
            Events.OpenDeadPanel(_houseplant);
        }
    }

    public void EnablePanel()
    {
        _panelEnabled = true;
    }
}
