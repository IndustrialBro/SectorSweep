using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Daggers", menuName = "Orders/Daggers")]
public class SwitchStateCommand : Command
{
    StateMachine mother;
    HealthScript hs;
    NavMeshAgent agent;
    EyeScript eye;
    public override void Execute(GameObject executor)
    {
        mother = executor.GetComponent<StateMachine>();
        hs = executor.GetComponent<HealthScript>();
        agent = executor.GetComponent<NavMeshAgent>();
        eye = executor.GetComponent<EyeScript>();

        switch (savedArgs[1])
        {
            case "free":
                mother.SwitchStates(new ReadyState(mother, hs, agent, eye)); break;
            case "sheathed":
                mother.SwitchStates(new UnreadyState(mother, hs, agent, eye)); break;
        }
    }

    public override bool HandleArgs(string[] args)
    {
        // daggers {jednotka} {free/sheathed}
        if(args.Length != 2) return false;
        if (!(args[0].StartsWith('u') || args[0] == "all")) return false;
        if (args[1] != "free" && args[1] != "sheathed") return false;

        savedArgs = args;
        specArg = 0;
        return true;
    }
}
