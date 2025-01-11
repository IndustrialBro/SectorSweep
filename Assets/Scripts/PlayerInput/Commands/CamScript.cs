using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CamViewToggle", menuName = "Orders/CamViewToggle")]
public class CamScript : Command
{
    public override void Execute(GameObject executor)
    {
        if(savedArgs.Length > 0)
        {
            CamControls.Instance.SwitchMount(savedArgs[0]);
        }
        else
            CamControls.Instance.ShowHideCam();
    }

    public override bool HandleArgs(string[] args)
    {
        if (args.Length > 1) { return false; }
        if (args.Length > 0 && !args[0].StartsWith("u")) { return false; }

        savedArgs = args;
        return true;
    }
}
