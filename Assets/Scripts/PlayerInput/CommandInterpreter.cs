using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CommandInterpreter : MonoBehaviour
{
    public static CommandInterpreter Instance { get; private set; }
    private CommandInterpreter() {}

    [SerializeField]
    List<string> commandNames = new List<string>();
    [SerializeField]
    List<Command> commands = new List<Command>();

    Dictionary<string, Command> comDic = new Dictionary<string, Command>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetUpComDic();

    }

    public void InterpretCommand(string command, string[] args, CommandListener[] units)
    {
       
        if (Instance.comDic.ContainsKey(command))
        {
            if (Instance.comDic[command].unitCommand)
            {
                if (Instance.comDic[command].HandleArgs(args))
                {
                    foreach (CommandListener unit in units)
                    {
                        if (unit != null)
                            unit.OnCommandSent(Instance.comDic[command]);
                    }
                }
                else
                {
                    string err = "";
                    foreach (string arg in args)
                    {
                        err += arg + " ";
                    }
                    CLIstateMachine.Instance.ShowOutput($"ERROR : Invalid arguments \"{err}\" for command \"{command}\"");
                }
            }
            else
            {
                CLIstateMachine.Instance.ShowOutput($"ERROR : Command \"{command}\" cannot be executed by units");
            }
        }
        else
        {
            CLIstateMachine.Instance.ShowOutput($"ERROR : Unrecognized command \"{command}\"");
        }
    }
    public void InterpretCommand(string command, string[] args)
    {
        if (Instance.comDic.ContainsKey(command))
        {
            if (!Instance.comDic[command].unitCommand)
            {
                if (Instance.comDic[command].HandleArgs(args))
                {
                    Instance.comDic[command].Execute(null);
                }
                else
                {
                    string err = "";
                    foreach (string arg in args)
                    {
                        err += arg + " ";
                    }
                    CLIstateMachine.Instance.ShowOutput($"ERROR : Invalid arguments \"{err}\" for command \"{command}\"");
                }
            }
            else
            {
                CLIstateMachine.Instance.ShowOutput($"ERROR : Command \"{command}\" requires units to execute");
            }
        }
        else
        {
            CLIstateMachine.Instance.ShowOutput($"ERROR : Unrecognized command \"{command}\"");
        }
    }
    void SetUpComDic()
    {
        for(int i = 0; i < commandNames.Count && i < commands.Count; i++)
        {
            Instance.comDic.Add(commandNames[i].ToLower(), commands[i]);
        }
    }
    public string UpdateHint(string[] unitlessInput)
    {
        if (unitlessInput.Length == 0) return "";
        char[] temp = unitlessInput[unitlessInput.Length-1].ToCharArray();
        if (unitlessInput.Length > 1)
        {
            if (comDic.ContainsKey(unitlessInput[0]))
            {
                Command c = comDic[unitlessInput[0]];
                for (int i = 0; i < c.Hints.Count; i++)
                {
                    if (CheckMatch(c.Hints[i].hint.ToCharArray(), temp)) return c.Hints[i].hint;
                }
            }
        }
        else
        {
            for(int i = 0; i < commandNames.Count; i++)
            {
                if (CheckMatch(commandNames[i].ToCharArray(), temp)) return commandNames[i];
            }
        }
        return "";
    }
    bool CheckMatch(char[] hint, char[] input)
    {
        if (hint.Length < input.Length) return false;
        
        for(int i = 0;i < input.Length; i++)
        {
            if (hint[i] != input[i]) return false;
        }
        return true;
    }
}
