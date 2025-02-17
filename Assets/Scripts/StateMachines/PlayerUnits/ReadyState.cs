using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReadyState : PlayerState
{
    public ReadyState(PUstatemachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
    {
    }


    public override void OnStateEnter()
    {
        Debug.Log($"{mother} has entered the Ready State");
        mother.TogglePointer();
        agent.speed = 3;
    }

    public override void OnStateExit()
    {
        mother.TogglePointer();
    }

    public override void OnUpdate()
    {
        if(eye.CheckForUnits(out GameObject[] go))
        {
            InCombatState ics = new InCombatState(mother, hs, agent, GetNearestUnit(go), eye, mother.GetComponentInChildren<GunScript>(), this);
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
