using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaeObjective : Objective
{
    public override void PrepareObjective()
    {
        EnemyManager.Instance.AllDead += EndObjective;
        EnemyManager.Instance.RequestSpawnEnemy(3, 10);
        CLIstateMachine.Instance.ShowOutput("NEW OBJECTIVE : Eliminate all threats in the area.");
    }
    protected override void EndObjective()
    {
        EnemyManager.Instance.AllDead -= EndObjective;
        CLIstateMachine.Instance.EnterQueryState("Objective 'Eliminate all threats' achieved. Choose a reward:",
                    new HAUOption("Heal all units"),
                    new IMHPOption("Increase max HP of your units"),
                    new IDOption("Increase damage dealt by your units")
                );
    }
}
public class ktObjective : Objective
{
    GameObject target;
    public override void PrepareObjective()
    {
        EnemyHealthScript.EnemyDied += EndObjective;
        target = EnemyManager.Instance.GetRandomEnemy();
        CLIstateMachine.Instance.ShowOutput("NEW OBJECTIVE : Eliminate specified target.");
    }

    void EndObjective(GameObject enemy)
    {
        if(enemy == target)
        {
            EnemyHealthScript.EnemyDied -= EndObjective;
            CLIstateMachine.Instance.EnterQueryState("Objective 'Eliminate target' achieved. Choose a reward:",
                    new HAUOption("Heal all units"),
                    new IMHPOption("Increase max HP of your units"),
                    new IDOption("Increase damage dealt by your units")
                );
        }
    }
    protected override void EndObjective(){}
}
public class ctfObjective : Objective
{
    public override void PrepareObjective()
    {
        FlagScript.FlagTaken += EndObjective;
        ObjectiveManager.Instance.SpawnFlag();
        CLIstateMachine.Instance.ShowOutput("NEW OBJECTIVE : Confiscate the contraband.");
    }
    protected override void EndObjective()
    {
        FlagScript.FlagTaken -= EndObjective;
        CLIstateMachine.Instance.EnterQueryState("Objective 'Confiscate the contraband' achieved. Choose a reward:",
                    new HAUOption("Heal all units"),
                    new IMHPOption("Increase max HP of your units"),
                    new IDOption("Increase damage dealt by your units")
                );
    }
}