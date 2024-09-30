using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : State
{
    EyeScript e;
    public ReadyState(StateMachine mother) : base(mother)
    {
    }

    public override void OnHit()
    {

    }

    public override void OnStateEnter()
    {
        e = mother.GetComponent<EyeScript>();
    }

    public override void OnUpdate()
    {
        if(e.CheckForUnits(out GameObject[] go))
        {
            InCombatState ics = new InCombatState(mother);
            mother.SwitchStates(ics);
        }
    }
}
