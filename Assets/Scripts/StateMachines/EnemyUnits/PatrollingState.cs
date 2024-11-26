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
        currDest = Vector3.zero;
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
            if (units.Length > GetTotalBravery() && GetEscapePoint(out Vector3 point))
            {
                //uteèe
                DistressedState d = new DistressedState(mother, hs, agent, eye, this);
                mother.SwitchStates(new RetreatState(mother, hs, agent, eye, point, d));
            }
            else
            {
                //jde bojovat
                mother.SwitchStates(new InCombatState(mother, hs, agent, units, eye, mother.GetComponent<GunScript>(), this));
            }
        }
    }
    int GetTotalBravery()
    {
        int tb = 0;
        Collider[] colls = Physics.OverlapSphere(mother.transform.position, 20);
        foreach(Collider coll in colls)
        {
            EUnitStateMachine temp = coll.GetComponent<EUnitStateMachine>();

            if (temp != null)
                tb += temp.bravery;
        }
        return tb;
    }
    bool GetEscapePoint(out Vector3 point)
    {
        Collider[] colls = Physics.OverlapSphere(mother.transform.position, 50f, 1 << LayerMask.NameToLayer("EscapePoints"));
        if(colls.Length > 0)
        {
            point = colls[0].transform.position;
            foreach(Collider candidate in colls)
            {
                if(Vector3.Distance(mother.transform.position, candidate.transform.position) > Vector3.Distance(mother.transform.position, point))
                    point = candidate.transform.position;
            }

            return true;
        }
        point = Vector3.zero;
        return false;
    }
}
