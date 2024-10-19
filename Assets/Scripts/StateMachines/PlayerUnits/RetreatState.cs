using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RetreatState : State
{
    public RetreatState(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
    {
    }


    public override void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
