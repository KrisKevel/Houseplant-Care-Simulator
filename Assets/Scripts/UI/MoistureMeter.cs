using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MoistureMeter : MonoBehaviour
{
    public TextMeshProUGUI MoistureLevel;
    public TextMeshProUGUI Status;


    private void Awake()
    {
        Events.OnPopUp += OpenMenu;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        //Currently buggy
        //HideIfClickedOutside(gameObject);

        if (Input.GetKeyDown("q"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Events.OnPopUp -= OpenMenu;
    }

    public void OpenMenu(float moisture)
    {
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);
        MoistureLevel.text = moisture.ToString();
        SetStatus(moisture);
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

    private void HideIfClickedOutside(GameObject panel)
    {
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main))
        {
            panel.SetActive(false);
        }
    }
}
