using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnreadyState : State
{
    public UnreadyState(StateMachine mother) : base(mother)
    {
    }


    public override void OnStateEnter()
    {

    }

    public override void OnUpdate()
    {

    }
    public override void OnHit()
    {
        InCombatState ics = new InCombatState(mother);
        mother.SwitchStates(ics);
    }
}
