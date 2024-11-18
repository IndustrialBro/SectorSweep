using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ComListThingamabob : MonoBehaviour
{
    public static ComListThingamabob Instance { get; private set; } = null;
    private ComListThingamabob() { Instance = this; }

    List<CommandListener> commandListeners = new List<CommandListener>();


    public void AddListener(CommandListener listener)
    {
        commandListeners.Add(listener);
    }
    public CommandListener GetListener(string id)
    {
        foreach(CommandListener listener in commandListeners)
        {
            if(listener.id == id) return listener;
        }
        return null;
    }
    public CommandListener[] GetListeners(string[] ids)
    {
        if (ids[0] == "all")
        {
            return commandListeners.ToArray();
        }
        else
        {
            List<CommandListener> l = new List<CommandListener>();

            foreach (string id in ids)
            {
                l.Add(GetListener(id));
            }

            return l.ToArray();
        }
    }
}