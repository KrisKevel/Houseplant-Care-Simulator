using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Destination destObject;
    private Transform _theDest;
    private bool _pickedUp = false;
    private bool _clickable = false;
    
    private GameObject[] _places;

    private Placement currentPosition;

    private void Awake()
    {
        destObject = FindObjectOfType<Destination>();
        _theDest = destObject.transform;
        Events.OnPlacePlant += PlacePlant;
        Events.OnPickUpPlant += PickPlantUp;
        _places = GameObject.FindGameObjectsWithTag("Place");
    }
    private void OnDestroy()
    {
        Events.OnPlacePlant -= PlacePlant;
        Events.OnPickUpPlant -= PickPlantUp;
    }

    void PickPlantUp(GameObject houseplant)
    {
        if (gameObject != houseplant) { return; }
        UpdateClickable();
        if (_clickable && !destObject.carrying)
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
        if (_pickedUp)
        {
            Placement place = GetNearestPoint(nearPoint);
            if (place == null) { return; }
            Vector3 finalPosition = place.transform.position;

            if (CheckIfFree(finalPosition))
            {
                gameObject.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                gameObject.transform.position = finalPosition;
                _pickedUp = false;
                destObject.carrying = false;
                currentPosition = place;
            }
        }
    }
    public Placement GetNearestPoint(Vector3 position)
    {
        foreach (GameObject place in _places)
        {
            Placement placement = place.GetComponent<Placement>();
            if (placement.CanBePlaced(position))
            {
                return placement;
            }
        }

        return null;
    }

    bool CheckIfFree(Vector3 point)
    {
        if (point.x == 0 && point.y == 0 && point.z == 0)
        {
            return false;
        }

        Collider[] intersecting = Physics.OverlapSphere(point, 0.01f);
        
        if(intersecting.Length == 0)
        {
            return true;
        }

        return intersecting[0].gameObject.tag != "Plant";
    }

    void UpdateClickable()
    {
        _clickable = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < GameManager.Instance.AOE;
    }

    public Placement GetCurrentPlacement()
    {
        return currentPosition;
    }
}
