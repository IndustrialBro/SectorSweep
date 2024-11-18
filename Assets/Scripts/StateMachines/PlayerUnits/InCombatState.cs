using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InCombatState : State
{
    GameObject target;
    GunScript gun;
    State nextState;
    float maxInterval = 2, currInterval;
    public InCombatState(StateMachine mother, HealthScript hs, NavMeshAgent agent, GameObject target, EyeScript eye, GunScript gun, State nextState) : base(mother, hs, agent, eye)
    {
        this.target = target;
        this.gun = gun;
        this.nextState = nextState;
    }


    public override void OnStateEnter()
    {
        agent.updateRotation = false;
        agent.speed = 1;

        currInterval = maxInterval;
        Debug.Log($"{mother} has entered the In Combat State");
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
        if (target == null)
        {
            mother.SwitchStates(nextState);
            return;
        }

        Vector3 targetDir = target.transform.position - mother.transform.position;
        targetDir = new(targetDir.x, 0, targetDir.z);

        if (mother.transform.forward != targetDir)
        {
            Vector3 newDir = Vector3.RotateTowards(mother.transform.forward, targetDir, 3 * Time.deltaTime, 0);

            mother.transform.rotation = Quaternion.LookRotation(newDir);
        }
        
        //Pokud je terè/cíl/whatever v urèitém úhlu vùèí mother.transform.forward tak ho napumpuj olovem
        if(Vector3.Angle(mother.transform.forward, targetDir) < 7)
        {
            gun.Fire(target.tag);
        }
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
            mother.SwitchStates(nextState);
    }
}
