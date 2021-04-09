using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public GameObject ObjectToPointAt;

    public void SetObject(GameObject objectToPointAt, bool rotate)
    {
        ObjectToPointAt = objectToPointAt;
        if (objectToPointAt == null) {
            HideArrow();
            return; 
        }

        Vector2 offset = new Vector3(-40, 120);

        if (rotate)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, -43.072f, 0);
        }
        else
        {
            offset += new Vector2(50, 50);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        Vector2 canvasPos;

        if (ObjectToPointAt.GetComponent<RectTransform>() == null)
        {
            canvasPos = Camera.main.WorldToScreenPoint(ObjectToPointAt.transform.position);
        }
        else
        {
            canvasPos = ObjectToPointAt.transform.position;
        }

        gameObject.transform.position = canvasPos + offset;
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
