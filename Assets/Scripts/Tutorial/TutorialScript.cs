using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : ObjectiveManager
{
    // Start is called before the first frame update
    void Start()
    {
        objectives = new Objective[]
        { 
            new ctfObjective(),
            new TutObjective("For your next objective you'll have to kill an enemy. Don't worry, your units are strong but a little oblivious." +
                             "\nInput '0' to continue.",
                             new NestedOption("continue"
                             , "Right now your units have their guard down. Use the daggers command to change that." +
                               "\nWrite 'all' (or the names of your units), then 'daggers free'." +
                               "\nYour units will now shoot enemies on sight. This slows them down however." +
                               " If you want them to move fast again, write the same command but write 'sheathed' instead of 'free'." +
                               "\n Input '0' to continue."
                             , new NextObjectiveOption("continue"))),
            new ktObjective(),
            new TutObjective("Next you'll have to kill all enemies in the area. However you will not be able to see them, since they're not specified targets." +
                             "\nUse the 'help' command to find commands that might help with that. Simply input 'help' with no units or arguments to see a list of commands." +
                             "\nWriting 'help' and then a name of a command will show you more information about that command." +
                             "\n Input '0' to continue."
                             , new NextObjectiveOption("continue")),
            new kaeObjective()
        };
        PrepQueue();
        CLIstateMachine.Instance.EnterQueryState("This text is written in the console output. Bellow it is the console input, which is where you are writing." +
                                                 "\nIf you write something and press enter, the console will process it and give you a result." +
                                                 "\nInput '0' to continue."
                                                 , new NestedOption("continue", 
                                                 "On the right is the mini map. It shows your units (red squares labeled u1 and u2) and tiles (grey squares)." +
                                                 "\nTo move your units use the marchto command. First write the names of the units you want to move (seperated by spaces)" +
                                                 " then 'marchto' and the name of the tile you want them to move to." +
                                                 "\nIf you want all units to move together, you can write 'all' instead of all the names." +
                                                 "\nUse the marchto command to complete the next objective." +
                                                 "\n Input '0' to continue."
                                                 , new NextObjectiveOption("continue")));
    }
    protected override void PrepQueue()
    {
        for(int i = 0; i < objectives.Length; i++)
        {
            oQueue.Enqueue(objectives[i]);
        }
    }
}
public class NextObjectiveOption : QOption
{
    public NextObjectiveOption(string desc) : base(desc)
    {
    }

    public override void DoStuff()
    {
        ObjectiveManager.Instance.NextObjective();
    }
}
public class NestedOption : QOption
{
    QOption[] nxtOpt;
    string nxtDesc;
    public NestedOption(string desc, string nxtDesc, params QOption[] nxtOpt) : base(desc)
    {
        this.nxtDesc = nxtDesc;
        this.nxtOpt = nxtOpt;
    }

    public override void DoStuff()
    {
        CLIstateMachine.Instance.EnterQueryState(nxtDesc, nxtOpt);
    }
}
public class TutObjective : Objective
{
    string desc;
    QOption[] options;
    public TutObjective(string desc, params QOption[] options) 
    {
        this.desc = desc;
        this.options = options;
    }
    public override void PrepareObjective()
    {
        CLIstateMachine.Instance.EnterQueryState(desc, options);
    }

    protected override void EndObjective(){}
}