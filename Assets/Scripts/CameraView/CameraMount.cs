using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMount : MonoBehaviour
{
    [field: SerializeField]
    public string id { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        CamControls.Instance.AddMount(this);
    }
}
