using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected StateMachine mother;
    protected HealthScript hs;
    protected NavMeshAgent agent;
    protected EyeScript eye;
    public bool canExit {  get; protected set; } = true;

    public State(StateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye)
    {
        this.mother = mother;
        this.hs = hs;
        this.agent = agent;
        this.eye = eye;
    }

    public abstract void OnUpdate();
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public virtual void OnHit(int dmg, GameObject attacker) 
    {
        hs.GetHurt(dmg);
    }
}
