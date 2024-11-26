using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class RetreatState : State
{
    Vector3 targetLoc;
    State nextState;
    public RetreatState(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye, Vector3 targetLoc, State nextState) : base(mother, hs, agent, eye)
    {
        this.targetLoc = targetLoc;
        this.nextState = nextState;
    }

    public override void OnStateEnter()
    {
        canExit = false;
        
        agent.speed = 6;
        agent.SetDestination(targetLoc);
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
            mother.SwitchStates(nextState);
        }
    }
}
