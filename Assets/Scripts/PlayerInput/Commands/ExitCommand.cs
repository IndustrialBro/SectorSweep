using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Exit", menuName = "Orders/Exit")]
public class ExitCommand : Command
{
    public override void Execute(GameObject executor)
    {
        CLIstateMachine.Instance.EnterQueryState("Where would you like to exit?",
            new ExitOption("Desktop"),
            new MainMenuOption("Main Menu"),
            new DontExitOption("Don't exit"));
    }

    public override bool HandleArgs(string[] args)
    {
        return args.Length == 0;
    }
}
public class ExitOption : QOption
{
    public ExitOption(string desc) : base(desc)
    {
    }

    public override void DoStuff()
    {
        GameManager.Instance.ExitProgramme();
    }
}
public class MainMenuOption : QOption
{
    public MainMenuOption(string desc) : base(desc)
    {
    }

    public override void DoStuff()
    {
        GameManager.Instance.ChangeScene("MainMenu");
    }
}
public class DontExitOption : QOption
{
    public DontExitOption(string desc) : base(desc)
    {
    }

    public override void DoStuff()
    {
    }
}