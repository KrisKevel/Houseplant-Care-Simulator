using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float _size = 1f;
    [SerializeField]
    private float _z = 1f;
    [SerializeField]
    private float _x = 1f;
    [SerializeField]
    private float _zSize = 1f;
    [SerializeField]
    private float _xSize = 1f;
    [SerializeField]
    private float _zOffset = 1f;
    [SerializeField]
    private float _xOffset = 1f;

    [SerializeField]
    [Range(0, 5)]
    private float _clickingRoomX = 1f;
    [SerializeField]
    [Range(0, 5)]
    private float _clickingRoomZ = 1f;

    [SerializeField]
    private float _height = 1f;


    private float xMax;
    private float zMax;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        xMax = _xSize + _x;
        zMax = _zSize + _z;
        if (position.x > xMax + _clickingRoomX || position.x < _x - _clickingRoomX
            || position.z > zMax + _clickingRoomZ || position.z < _z - _clickingRoomZ)
        {
            return new Vector3();
        }

        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / _size);
        int yCount = Mathf.RoundToInt(position.y / _size);
        int zCount = Mathf.RoundToInt(position.z / _size);

        Vector3 result = new Vector3(
            (float)xCount * _size,
            _height,
            (float)zCount * _size);

        result.y = _height + 0.5f;
        result.x += transform.position.x + _xOffset;
        result.z += transform.position.z + _zOffset;

        return result;
    }

    private void OnDrawGizmos()
    {
        xMax = _xSize + _x;
        zMax = _zSize + _z;
        Gizmos.color = Color.yellow;
        for (float x = _x; x < xMax; x += _size)
        {
            for (float z = _z; z < zMax; z += _size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, _height, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
