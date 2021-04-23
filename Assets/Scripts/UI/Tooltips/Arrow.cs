using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public float yOffset;
    public float xOffset;

    [HideInInspector]
    public GameObject ObjectToPointAt;
    [HideInInspector]
    public Rect screenRes;

    void Awake()
    {
        screenRes.x = Screen.width;
        screenRes.y = Screen.height;
    }

    public void SetObject(GameObject objectToPointAt, bool rotate)
    {
        ObjectToPointAt = objectToPointAt;
        if (objectToPointAt == null) {
            HideArrow();
            return; 
        }

        float _xOffset;
        float _yOffset;

        if (rotate)
        {
            _xOffset = -screenRes.x * (xOffset);
            _yOffset = screenRes.y * (yOffset-0.01f);
            gameObject.transform.rotation = new Quaternion(0, 180, -43.072f, 0);
        }
        else
        {
            _xOffset = screenRes.x * xOffset;
            _yOffset = screenRes.y * yOffset;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        Vector3 canvasPos;

        if (ObjectToPointAt.GetComponent<RectTransform>() == null)
        {
            canvasPos = Camera.main.WorldToScreenPoint(ObjectToPointAt.transform.position);
        }
        else
        {
            canvasPos = ObjectToPointAt.transform.position;
        }

        canvasPos.x += _xOffset;
        canvasPos.y += _yOffset;

        transform.position = canvasPos;
        ShowArrow();
    }

    public void ShowArrow()
    {
        gameObject.SetActive(true);
    }

    public void HideArrow()
    {
        gameObject.SetActive(false);
    }
}
