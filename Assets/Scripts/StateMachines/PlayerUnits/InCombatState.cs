using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InCombatState : State
{
    GameObject target;
    float maxInterval = 2, currInterval;
    public InCombatState(StateMachine mother, HealthScript hs, NavMeshAgent agent, GameObject target, EyeScript eye) : base(mother, hs, agent, eye)
    {
        this.target = target;
    }


    public override void OnStateEnter()
    {
        agent.updateRotation = false;
    }

    public override void OnStateExit()
    {
        agent.updateRotation = true;
    }

    public override void OnUpdate()
    {
        UpdateRotation();

        CheckIfTargetIsVisible();
    }
    void UpdateRotation()
    {
        Vector3 targetDir = target.transform.position - mother.transform.position;

        if (mother.transform.forward != targetDir)
        {
            Vector3 newDir = Vector3.RotateTowards(mother.transform.forward, targetDir, 6 * Time.deltaTime, 0);

            mother.transform.rotation = Quaternion.LookRotation(newDir);
        }
        //Pokud je terè/cíl/whatever v urèitém úhlu vùèí mother.transform.forward tak ho napumpuj olovem
        
    }
    void CheckIfTargetIsVisible()
    {
        if(eye.CheckIfVisible(target))
        {
            currInterval = maxInterval;
        } else
        {
            currInterval -= Time.deltaTime;
        }

        if (currInterval < 0)
            mother.SwitchStates(new ReadyState(mother, hs, agent, eye));
    }
}
