using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnreadyState : PlayerState
{
    public UnreadyState(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
    {
    }


    public override void OnStateEnter()
    {
        Debug.Log($"{mother} has entered the Unready State");

        agent.speed = 5;
    }
    public override void OnStateExit()
    {

    }

    public override void OnUpdate()
    {

    }

}
