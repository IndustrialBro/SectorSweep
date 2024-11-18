using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float size { get; private set; } = 5;
    TMP_Text text;

    public static implicit operator Vector3(Tile t) => t.transform.position;

    public bool IsAboveGround()
    {
        if (Physics.Raycast(gameObject.transform.position, -transform.up, out RaycastHit hit))
        {
            transform.position = hit.point;
            text = GetComponentInChildren<TMP_Text>();
            return true;
        }
        return false;
    }
    public void SetName(string name)
    {
        this.name = name;
        text.text = name;  
    }
}
