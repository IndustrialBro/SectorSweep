using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject
{
    [field : SerializeField]
    public bool unitCommand { get; protected set; }
    public string[] savedArgs { get; protected set; }
    public abstract bool HandleArgs(string[] args);
    public abstract void Execute(GameObject executor);
    
    [field : SerializeField]
    protected List<CommandHint> hints = new List<CommandHint>();
    public IReadOnlyList<CommandHint> Hints { get {  return hints; } }
    
    [field : SerializeField]
    public string shortDesc { get; protected set; }
    [field : SerializeField, Multiline]
    public string longDesc { get; protected set; }
    public Command Duplicate()
    {
        return this.MemberwiseClone() as Command;
    }
}

[Serializable]
public class CommandHint
{
    [field : SerializeField]
    public int sequence { get; private set; }
    [field : SerializeField]
    public string hint { get; private set; }
}