using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandListener : MonoBehaviour
{
    [field : SerializeField]
    public string id { get; private set; }
    private void Start()
    {
        ComListThingamabob.Instance.AddListener(this);
    }

    public void OnCommandSent(Command command)
    {
        command.Duplicate().Execute(gameObject);
    }
}
