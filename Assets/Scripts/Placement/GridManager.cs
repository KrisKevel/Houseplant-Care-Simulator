using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private GameObject[] _objectsWithGrid;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _objectsWithGrid = GameObject.FindGameObjectsWithTag("Grid");
    }

    public Vector3 GetNearestPoint(Vector3 position)
    {
        print("Grids: " + _objectsWithGrid.Length);
        foreach(GameObject grid in _objectsWithGrid)
        {
            Vector3 result = grid.GetComponent<Grid>().GetNearestPointOnGrid(position);
            
            if (PointFound(result))
            {
                return result;
            }
        }

        return new Vector3();
    }

    bool PointFound(Vector3 position)
    {
        return position.x != 0 || position.y != 0 || position.y != 0;
    }
}
