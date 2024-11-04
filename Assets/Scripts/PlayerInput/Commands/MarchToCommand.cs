using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MarchTo", menuName = "Orders/MarchTo")]
public class MarchToCommand : Command
{
    public override void Execute(GameObject executor)
    {
        Tile targetTile = GridDoohickey.Instance.RequestTile(savedArgs[0]);

        if (targetTile != null )
        {
            NavMeshAgent agent = executor.GetComponent<NavMeshAgent>();
            float x = targetTile.transform.position.x + Random.Range(-1.5f, 1.5f);
            float z = targetTile.transform.position.z + Random.Range(-1.5f, 1.5f);
            Vector3 targetPos = new Vector3( x, targetTile.transform.position.y, z);
            agent.SetDestination(targetPos);
        }
    }

    public override bool HandleArgs(string[] args)
    {
        if(args.Length != 1) return false;
        if (!args[0].StartsWith("t")) return false;

        savedArgs = args;
        return true;
    }
}
