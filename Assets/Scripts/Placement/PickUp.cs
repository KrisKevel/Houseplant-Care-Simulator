using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Destination destObject;
    private Transform _theDest;
    private bool _pickedUp = false;
    private bool _clickable = false;

    private void Awake()
    {
        destObject = FindObjectOfType<Destination>();
        _theDest = destObject.transform;
    }

    private void Update()
    {
        if (_pickedUp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hitInfo))
                {
                    PlacePlant(hitInfo.point);
                }
            }
        }
    }

    void OnMouseDown()
    {
        UpdateClickable();
        if (!_pickedUp && _clickable && !destObject.carrying)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = _theDest.position;
            gameObject.transform.parent = _theDest;
            gameObject.transform.rotation = new Quaternion();
            _pickedUp = true;
            destObject.carrying = true;
        }
    }

    void PlacePlant(Vector3 nearPoint)
    {
        UpdateClickable();
        var finalPosition = GridManager.Instance.GetNearestPoint(nearPoint);
        if (CheckIfFree(finalPosition) && _clickable)
        {
            gameObject.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.position = finalPosition;
            _pickedUp = false;
            destObject.carrying = false;
        }
    }

    bool CheckIfFree(Vector3 point)
    {
        if (point == null)
        {
            return false;
        }
        Collider[] intersecting = Physics.OverlapSphere(point, 0.01f);
        return intersecting.Length == 0;
    }

    void UpdateClickable()
    {
        _clickable = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < GameManager.Instance.AOE;
    }
}
