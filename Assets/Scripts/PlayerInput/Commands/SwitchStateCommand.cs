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

        switch (savedArgs[0])
        {
            case "free":
                mother.SwitchStates(new ReadyState(mother, hs, agent, eye)); break;
            case "sheathed":
                mother.SwitchStates(new UnreadyState(mother, hs, agent, eye)); break;
        }
    }

    public override bool HandleArgs(string[] args)
    {
        // {jednotka} daggers {free/sheathed}
        if (args.Length != 1) return false;
        if (args[0] != "free" && args[0] != "sheathed") return false;

        savedArgs = args;
        return true;
    }
}
