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
        Vector3 position = ObjectToPointAt.transform.position;
        position += new Vector3(-500, -100, 0);
        gameObject.transform.localPosition = position;
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
