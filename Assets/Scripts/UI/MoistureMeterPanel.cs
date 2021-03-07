using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MoistureMeterPanel : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI MoistureLevel;
    public TextMeshProUGUI Status;
    public TextMeshProUGUI LightLevel;

    private HouseplantHealth _houseplant;

    private bool _waterButtonPressed = false;
    private bool _mouseIsOver = false;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        Events.OnOpenMoistureMeter += OpenMenu;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            gameObject.SetActive(false);
        }

        if (_waterButtonPressed)
        {
            _houseplant.IncreaseWaterLevel();
        }

        UpdateData();
    }

    private void OnDestroy()
    {
        Events.OnOpenMoistureMeter -= OpenMenu;
    }

    //https://answers.unity.com/questions/947856/how-to-detect-click-outside-ui-panel.html?page=1&pageSize=5&sort=votes 
    //Ziplock9000's answer + BluishGreenPro's comment
    public void OnDeselect(BaseEventData eventData)
    {
        if (!_mouseIsOver)
            gameObject.SetActive(false);
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
        EventSystem.current.SetSelectedGameObject(gameObject);

        _houseplant = houseplant;
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);

        UpdateData();
    }


    private void UpdateData()
    {
        float moisture = _houseplant.GetWaterLevel();
        MoistureLevel.text = System.Math.Round(moisture, 2).ToString();
        SetStatus(moisture);

        float light = _houseplant.GetLightLevel();
        LightLevel.text = System.Math.Round(light, 2).ToString();
    }

    private void SetStatus(float moisture)
    {
        if(moisture < 33.3)
        {
            Status.text = "Dry";
            Status.color = Color.red;
        }
        else if(moisture < 66.6)
        {
            Status.text = "Moist";
            Status.color = Color.green;
        }
        else
        {
            Status.text = "Wet";
            Status.color = Color.blue;
        }
    }

    public void WaterButtonDown()
    {
        _waterButtonPressed = true;
    }

    public void WaterButtonUp()
    {
        _waterButtonPressed = false;
    }
}
