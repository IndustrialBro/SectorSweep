using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaeObjective : Objective
{
    public override void PrepareObjective()
    {
        EnemyManager.Instance.AllDead += () => {
            CLIstateMachine.Instance.EnterQueryState("Objective 'kill all enemies' achieved. Choose a reward:", 
                    new HAUOption("Heal all units"), 
                    new IMHPOption("Increase max HP of your units"),
                    new IDOption("Increase damage dealt by your units")
                );
        };
    }
}
