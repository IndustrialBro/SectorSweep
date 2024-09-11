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

    List<CommandListener> listeners = new List<CommandListener>();

    private void Start()
    {
        SetUpComDic();

    }

    public void SetCLI(CLIscript cli)
    {
        this.cli = cli;
    }
    public void InterpretCommand(string command, string[] args)
    {
       
        if (Instance.comDic.ContainsKey(command))
        {
            if (Instance.comDic[command].HandleArgs(args))
            {
                if (Instance.comDic[command].specArg > -1)
                {
                    int specArg = Instance.comDic[command].specArg;
                    foreach (CommandListener l in listeners)
                    {
                        if (l.id == Instance.comDic[command].savedArgs[specArg])
                        {
                            Instance.comDic[command].Execute(l.gameObject);
                        }
                    }
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

    public void AddListener(CommandListener listener)
    {
        listeners.Add(listener);
    }

}
