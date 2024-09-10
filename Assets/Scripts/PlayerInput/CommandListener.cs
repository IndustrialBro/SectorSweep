using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandListener : MonoBehaviour
{
    [field : SerializeField]
    public string id { get; private set; }
    private void Start()
    {
        CommandInterpreter.Instance.AddListener(this);
    }
}
