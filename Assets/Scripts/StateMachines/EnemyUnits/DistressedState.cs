using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistressedState : EnemyState
{
    public DistressedState(EUnitStateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye, EnemyState nextState) : base(mother, hs, agent, eye)
    {
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public override void OnUpdate()
    { 

    }
}
