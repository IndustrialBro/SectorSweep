using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currState = null;
    // Start is called before the first frame update
    protected void Start()
    {
        InitStates();
    }

    // Update is called once per frame
    protected void Update()
    {
        currState.OnUpdate();
    }

    protected abstract void InitStates();
    public void SwitchStates(State newState)
    {
        if(currState.canExit)
        {
            currState.OnStateExit();
            currState = newState;
            currState.OnStateEnter();
        }
    }
    public void OnHit(int dmg, GameObject attacker)
    {
        currState.OnHit(dmg, attacker);
    }
    public override string ToString()
    {
        return $"{gameObject.name}'s state machine";
    }
}
