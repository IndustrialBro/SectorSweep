using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float size { get; private set; } = 5;

    public Vector3 GetRandPosInTile()
    {
        float x = transform.position.x + Random.Range(-size, size);
        float y = transform.position.z + Random.Range(-size, size);

        return new Vector3(x, transform.position.y, y);
    }

    public bool IsAboveGround()
    {
        if (Physics.Raycast(gameObject.transform.position, -transform.up, out RaycastHit hit))
        {
            transform.position = hit.point;
            return true;
        }
        return false;
    }
}
