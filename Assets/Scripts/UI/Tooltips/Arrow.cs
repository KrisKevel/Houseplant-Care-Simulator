using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject ObjectToPointAt;

    public void SetObject(GameObject objectToPointAt)
    {
        ObjectToPointAt = objectToPointAt;
        if (objectToPointAt == null) {
            HideArrow();
            return; 
        }

        Vector2 canvasPos;

        if (ObjectToPointAt.GetComponent<RectTransform>() == null)
        {
            // https://forum.unity.com/threads/create-ui-health-markers-like-in-world-of-tanks.432935/?_ga=2.129965769.417841106.1617821738-405608760.1591295140
            // Offset position above object bbox (in world space)
            float offsetPosY = ObjectToPointAt.transform.position.y + 1.5f;

            // Final position of marker above GO in world space
            Vector3 offsetPos = new Vector3(ObjectToPointAt.transform.position.x, offsetPosY, ObjectToPointAt.transform.position.z);

            // Calculate *screen* position (note, not a canvas/recttransform position)
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

            // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
            RectTransformUtility.ScreenPointToLocalPointInRectangle(gameObject.GetComponentInParent<RectTransform>(), screenPoint, null, out canvasPos);
            canvasPos += new Vector2(690, 450);
            gameObject.transform.position = canvasPos;
        }
        else
        {
            gameObject.transform.position = ObjectToPointAt.transform.position + new Vector3(50, 210);
        }

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
