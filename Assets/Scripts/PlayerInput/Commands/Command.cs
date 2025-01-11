using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject
{
    [field: SerializeField]
    public bool unitCommand { get; protected set; }
    public string[] savedArgs { get; protected set; }
    public abstract bool HandleArgs(string[] args);
    public abstract void Execute(GameObject executor);
    public Command Duplicate()
    {
        return this.MemberwiseClone() as Command;
    }
}
