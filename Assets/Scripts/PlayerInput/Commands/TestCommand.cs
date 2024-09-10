using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Test", menuName = "Orders/test")]
public class TestCommand : Command
{
    public override void Execute(GameObject executor)
    {
        float dist = float.Parse(savedArgs[1]);
        Vector3 oPos = executor.GetComponent<Transform>().position;
        Vector3 nPos = new Vector3(oPos.x, oPos.y + dist, oPos.z);
        executor.GetComponent<Transform>().position = nPos;
    }

    public override bool HandleArgs(string[] args)
    {
        if(args.Length != 2)
        {
            return false;
        }
        if (args[0].Length != 2)
        {
            return false;
        }
        if (!float.TryParse(args[1], out float value))
        {
            return false;
        }

        savedArgs = args;
        return true;
    }
}
