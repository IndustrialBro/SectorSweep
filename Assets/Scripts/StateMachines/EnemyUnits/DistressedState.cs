using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistressedState : EnemyState
{
    // ètyøicet pìt stupòù v radiánech (pí ètvrtin) nekontrolujte pøesnost plsky plsky a pokud ji kontrolovat budete tak o tom neøíkejte panu Ostrovidovi
    float checkAngle = 0.785f, timer = 15f;
    Vector3[] dirs = new Vector3[2];
    short curDir = 0;
    EnemyState nextState;
    public DistressedState(EUnitStateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye, EnemyState nextState) : base(mother, hs, agent, eye)
    {
        this.nextState = nextState;
    }

    public override void OnStateEnter()
    {
        agent.updateRotation = false;
        dirs[0] = mother.transform.right * Mathf.Tan(checkAngle) - mother.transform.forward;
        dirs[1] = -mother.transform.right * Mathf.Tan(checkAngle) - mother.transform.forward;
    }
    public override void OnUpdate()
    { 
        UpdateRotation();

        CheckForHostiles();

        timer -= Time.deltaTime;
        if (timer < 0) 
            mother.SwitchStates(nextState);
    }

    public override void OnStateExit()
    {
        agent.updateRotation = true;

    }

    void UpdateRotation()
    {
        if (Vector3.Angle(mother.transform.forward, dirs[curDir]) > 1)
        {
            Vector3 newDir = Vector3.RotateTowards(mother.transform.forward, dirs[curDir], 1 * Time.deltaTime, 0);
            mother.transform.rotation = Quaternion.LookRotation(newDir);
        }
        else
        {
            curDir++;
            if (curDir > 1)
                curDir = 0;
        }
    }
    void CheckForHostiles()
    {
        if(eye.CheckForUnits(out GameObject[] go))
        {
            mother.SwitchStates(new InCombatState(mother, hs, agent, go, eye, mother.GetComponent<GunScript>(), this));
        }
    }
}
