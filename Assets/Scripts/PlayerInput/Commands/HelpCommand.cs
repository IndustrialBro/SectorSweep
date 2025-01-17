using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Help", menuName = "Orders/Help")]
public class HelpCommand : Command
{
    public override void Execute(GameObject executor)
    {
        if(savedArgs.Length == 0)
            CLIstateMachine.Instance.ShowOutput(CommandInterpreter.Instance.GetCommandDesc());
        else
            CLIstateMachine.Instance.ShowOutput(CommandInterpreter.Instance.GetCommandDesc(savedArgs[0]));
    }

    public override bool HandleArgs(string[] args)
    {
        savedArgs = args;
        if (args.Length == 0) return true;
        if(args.Length == 1 && CommandInterpreter.Instance.IsCommand(args[0])) { return true; }
        return false;
    }
}
