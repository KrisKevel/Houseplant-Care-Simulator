using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float _size = 1f;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / _size);
        int yCount = Mathf.RoundToInt(position.y / _size);
        int zCount = Mathf.RoundToInt(position.z / _size);

        Vector3 result = new Vector3(
            (float)xCount * _size,
            0,
            (float)zCount * _size);

        result += transform.position;
        result.y = 0.5f;
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 40; x += _size)
        {
            for (float z = 0; z < 40; z += _size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
