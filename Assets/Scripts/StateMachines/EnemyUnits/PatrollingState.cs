using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingState : EnemyState
{
    Vector3 currDest;
    public PatrollingState(EUnitStateMachine mother, HealthScript hs, NavMeshAgent agent, EyeScript eye) : base(mother, hs, agent, eye)
    {
    }

    public override void OnStateEnter()
    {
        agent.speed = 5;
        //currDest = GridDoohickey.Instance.GetRandomTile();
        //agent.SetDestination(currDest);
    }

    public override void OnStateExit()
    {

    }

    public override void OnUpdate()
    {
        if(Vector3.Distance(mother.transform.position, currDest) < agent.stoppingDistance || currDest == Vector3.zero)
        {
            currDest = GridDoohickey.Instance.GetRandomTile();
            agent.SetDestination(currDest);
        }


        if(eye.CheckForUnits(out GameObject[] units))
        {
            if (units.Length > GetTotalBravery())
            {
                //uteèe
                Debug.Log("Utíkej, Forreste, utíkej!");
            }
            else
            {
                //jde bojovat
                mother.SwitchStates(new InCombatState(mother, hs, agent, GetNearestUnit(units), eye, mother.GetComponent<GunScript>(), this));
            }
        }
    }
    int GetTotalBravery()
    {
        int tb = mother.bravery;
        Collider[] colls = Physics.OverlapSphere(mother.transform.position, 20);
        foreach(Collider coll in colls)
        {
            EUnitStateMachine temp = coll.GetComponent<EUnitStateMachine>();

            if (temp != null)
                tb += temp.bravery;
        }
        return tb;
    }
    GameObject GetNearestUnit(GameObject[] go)
    {
        GameObject nearest = go[0];
        foreach(GameObject candidate in go)
        {
            if(Vector3.Distance(mother.transform.position, candidate.transform.position) < Vector3.Distance(mother.transform.position, nearest.transform.position))
            {
                nearest = candidate;
            }
        }
        return nearest;
    }
}
