using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RetreatState : State
{
    Vector3 targetLoc;
    public RetreatState(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye, Vector3 targetLoc) : base(mother, hs, agent, eye)
    {
        this.targetLoc = targetLoc;
    }


    public override void OnStateEnter()
    {
        canExit = false;
        
        agent.speed = 6;
        Debug.Log($"{mother} has entered the Retreat State");
    }

    public override void OnStateExit()
    {

    }

    public override void OnUpdate()
    {
        if(Vector3.Distance(mother.transform.position, targetLoc) < 3)
        {
            canExit = true;
            mother.SwitchStates(new UnreadyState(mother, hs, agent, eye));
        }
    }
}
