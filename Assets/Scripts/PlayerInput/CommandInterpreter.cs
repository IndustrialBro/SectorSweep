using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CommandInterpreter : MonoBehaviour
{
    public static CommandInterpreter Instance { get; private set; }
    private CommandInterpreter() { if(Instance == null)Instance = this; }

    [SerializeField]
    List<string> commandNames = new List<string>();
    [SerializeField]
    List<Command> commands = new List<Command>();

    Dictionary<string, Command> comDic = new Dictionary<string, Command>();

    CLIscript cli;

    private void Start()
    {
        SetUpComDic();

    }

    public void SetCLI(CLIscript cli)
    {
        this.cli = cli;
    }
    public void InterpretCommand(string command, string[] args, CommandListener[] units)
    {
       
        if (Instance.comDic.ContainsKey(command))
        {
            if (Instance.comDic[command].HandleArgs(args))
            {
                foreach(CommandListener unit in units)
                {
                    unit.OnCommandSent(Instance.comDic[command]);
                }
            }
            else
            {
                cli.ShowOutput($"ERROR : Invalid arguments \"{args}\" for command \"{command}\"");
            }
        }
        else
        {
            cli.ShowOutput($"ERROR : Unrecognized command \"{command}\"");
        }
    }

    void SetUpComDic()
    {
        for(int i = 0; i < commandNames.Count && i < commands.Count; i++)
        {
            Instance.comDic.Add(commandNames[i].ToLower(), commands[i]);
        }
    }
}
