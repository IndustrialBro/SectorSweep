using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PUstatemachine : StateMachine
{
    UnreadyState defaultState;
    protected override void InitStates()
    {
        defaultState = new(this, GetComponent<HealthScript>(), GetComponent<NavMeshAgent>(), GetComponent<EyeScript>());

        currState = defaultState;
        currState.OnStateEnter();
    }
}
