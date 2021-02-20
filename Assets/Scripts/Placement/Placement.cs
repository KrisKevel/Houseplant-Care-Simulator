using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public float RadiusOfInteraction = 1.0f;

    public bool CanBePlaced(Vector3 clickPos)
    {
        float distanceToClick = Vector3.Distance(gameObject.transform.position, clickPos);
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);

        return distanceToClick < RadiusOfInteraction &&
            distanceToPlayer < GameManager.Instance.AOE;
    }
}
