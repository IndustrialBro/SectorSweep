using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitFollower : MonoBehaviour
{
    FollowedUnit utf;

    // Update is called once per frame
    void Update()
    {
        if(utf != null)
        {
            transform.position = utf.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FollowUnit(FollowedUnit unit)
    {
        utf = unit;
        GetComponentInChildren<TMP_Text>().text = unit.unitName;
    }
}
