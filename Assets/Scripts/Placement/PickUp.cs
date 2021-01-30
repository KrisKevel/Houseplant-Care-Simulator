using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform theDest;

    private void Awake()
    {
        theDest = GameObject.Find("Destination").transform;
    }

    void OnMouseDown()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.position = theDest.position;
        gameObject.transform.parent = theDest;
    }

    void OnMouseUp()
    {
        gameObject.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
