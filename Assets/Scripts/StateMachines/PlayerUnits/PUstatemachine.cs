using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PUstatemachine : StateMachine
{
    [SerializeField]
    GameObject pointer;
    UnreadyState defaultState;
    protected override void InitStates()
    {
        TogglePointer();

        defaultState = new(this, GetComponent<HealthScript>(), GetComponent<NavMeshAgent>(), GetComponent<EyeScript>());

        currState = defaultState;
        currState.OnStateEnter();
    }
    public void TogglePointer()
    {
        pointer.SetActive(!pointer.activeSelf);
    }
}
