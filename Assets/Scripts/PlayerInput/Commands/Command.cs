using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject
{
    public string[] savedArgs { get; protected set; }
    public int specArg { get; protected set; } = -1;
    public abstract bool HandleArgs(string[] args);
    public abstract void Execute(GameObject executor);
}
