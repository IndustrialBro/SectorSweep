using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagScript : MonoBehaviour
{
    public static event UnityAction FlagTaken;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FlagTaken?.Invoke();

            Destroy(gameObject);
        }
    }
}
