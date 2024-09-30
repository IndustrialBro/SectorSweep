using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine mother;

    public State(StateMachine mother)
    {
        this.mother = mother;
    }

    public abstract void OnUpdate();
    public abstract void OnStateEnter();
    public abstract void OnHit();
}
