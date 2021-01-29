using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    public Grid FloorGrid;
    public GameObject PlantPrefab;

    // Update is called once per frame
    void Update()
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

    void PlacePlant(Vector3 nearPoint)
    {
        var finalPosition = FloorGrid.GetNearestPointOnGrid(nearPoint);
        Instantiate<GameObject>(PlantPrefab).transform.position = finalPosition;
    }
}
