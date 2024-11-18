using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EUnitStateMachine : StateMachine
{
    PatrollingState defaultState;
    [field : SerializeField]
    public int bravery {  get; private set; }
    protected override void InitStates()
    {
        defaultState = new(this, GetComponent<HealthScript>(), GetComponent<NavMeshAgent>(), GetComponent<EyeScript>());

        currState = defaultState;
        currState.OnStateEnter();
    }
}
