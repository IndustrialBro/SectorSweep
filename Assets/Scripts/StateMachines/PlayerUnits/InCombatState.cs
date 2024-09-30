using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCombatState : State
{
    EyeScript e;

    public InCombatState(StateMachine mother) : base(mother)
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
            //pøidá detekované jednotky do seznamu cílù
        }
        else
        {
            ReadyState rs = new ReadyState(mother);
            mother.SwitchStates(rs);
        }
    }
}
