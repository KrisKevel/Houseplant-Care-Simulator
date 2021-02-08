using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private GameObject[] _places;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _places = GameObject.FindGameObjectsWithTag("Place");
    }

    public Vector3 GetNearestPoint(Vector3 position)
    {
        foreach(GameObject place in _places)
        {
            if (place.GetComponent<Placement>().CanBePlaced(position))
            {
                return place.transform.position;
            }
        }

        return new Vector3();
    }
}
