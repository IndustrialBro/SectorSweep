using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : State
{
    protected new EUnitStateMachine mother;
    protected EnemyState(EUnitStateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
    {
        this.mother = mother;
    }

    public override void OnHit(int dmg, GameObject attacker)
    {
        base.OnHit(dmg, attacker);
        PatrollingState ps = new PatrollingState(mother, hs, agent, eye);
        DistressedState d = new DistressedState(mother, hs, agent, eye, ps);
        InCombatState ics = new InCombatState(mother, hs, agent, attacker, eye, mother.GetComponent<GunScript>(), d);
        mother.SwitchStates(ics);
    }
}
