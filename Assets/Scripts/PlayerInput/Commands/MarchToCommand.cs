using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MarchTo", menuName = "Orders/MarchTo")]
public class MarchToCommand : Command
{
    public override void Execute(GameObject executor)
    {
        Tile targetTile = GridDoohickey.Instance.RequestTile(savedArgs[1]);

        if (targetTile != null )
        {
            NavMeshAgent agent = executor.GetComponent<NavMeshAgent>();
            agent.SetDestination(targetTile.transform.position);
        }
    }

    public override bool HandleArgs(string[] args)
    {
        if(args.Length != 2) return false;
        if (!args[0].StartsWith("u")) return false;
        if (!args[1].StartsWith("t")) return false;

        savedArgs = args;
        return true;
    }
}
