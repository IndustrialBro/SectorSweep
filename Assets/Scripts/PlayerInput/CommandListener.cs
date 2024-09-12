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
        CommandInterpreter.Instance.SendCommand += OnCommandSent;
    }

    private void OnCommandSent(Command command)
    {
        int i = command.specArg;
        if (i > -1 && (command.savedArgs[i] == id || command.savedArgs[i] == "all")) 
        {
            command.Duplicate().Execute(gameObject);
        }
    }
}
