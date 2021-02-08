using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public float RadiusOfInteraction = 1.0f;

    public bool CanBePlaced(Vector3 clickPos)
    {
        float distance = Vector3.Distance(gameObject.transform.position, clickPos);

        if(distance > GameManager.Instance.AOE)
        {
            return false;
        }

        return true;
    }
}
