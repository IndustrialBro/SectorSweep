using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUstatemachine : StateMachine
{
    UnreadyState defaultState;
    protected override void InitStates()
    {
        defaultState = new(this);

        currState = defaultState;
    }
}
