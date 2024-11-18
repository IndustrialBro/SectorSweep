using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class PlayerState : State
{
    protected PlayerState(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
    {
    }
    public override void OnHit(int dmg, GameObject attacker)
    {
        base.OnHit(dmg, attacker);

        Debug.Log("Player unit hit");

        ReadyState rs = new ReadyState(mother, hs, agent, eye);
        InCombatState ics = new InCombatState(mother, hs, agent, attacker, eye, mother.GetComponentInChildren<GunScript>(), rs);
        mother.SwitchStates(ics);
    }
}
