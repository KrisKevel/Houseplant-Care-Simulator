using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Grid Grid;
    private Transform _theDest;
    private bool _pickedUp = false;

    private void Awake()
    {
        _theDest = GameObject.Find("Destination").transform;
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
        if (!_pickedUp)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = _theDest.position;
            gameObject.transform.parent = _theDest;
            _pickedUp = true;
        }
    }

    void PlacePlant(Vector3 nearPoint)
    {
        var finalPosition = Grid.GetNearestPointOnGrid(nearPoint);
        if (CheckIfFree(finalPosition) && finalPosition.z > -3f)
        {
            gameObject.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.position = finalPosition;
            _pickedUp = false;
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
}
