using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnreadyState : State
{
    public UnreadyState(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
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

    }
    public override void OnHit(int dmg, GameObject attacker)
    {
        base.OnHit(dmg, attacker);
        InCombatState ics = new InCombatState(mother, hs, agent, attacker, eye);
        mother.SwitchStates(ics);
    }

}
