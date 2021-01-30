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
        if(CheckIfFree(finalPosition) && finalPosition.z > -3f)
        {
            Instantiate(PlantPrefab).transform.position = finalPosition;
        }
    }

    bool CheckIfFree(Vector3 point)
    {
        Collider[] intersecting = Physics.OverlapSphere(point, 0.01f);
        print(intersecting.Length);
        if (intersecting.Length == 0)
        {
            return true;
        }
        else
        {

            return false;
        }
    }
}
