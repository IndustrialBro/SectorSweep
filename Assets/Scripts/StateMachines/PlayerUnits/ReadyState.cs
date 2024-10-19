using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReadyState : State
{
    public ReadyState(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
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
        if(eye.CheckForUnits(out GameObject[] go))
        {
            InCombatState ics = new InCombatState(mother, hs, agent, GetNearestUnit(go), eye);
            mother.SwitchStates(ics);
        }
    }
    GameObject GetNearestUnit(GameObject[] units)
    {
        GameObject nearest = units[0];

        foreach(GameObject unit in units)
        {
            if(Vector3.Distance(mother.transform.position, unit.transform.position) < Vector3.Distance(mother.transform.position, nearest.transform.position))
                nearest = unit;
        }

        return nearest;
    }
}
