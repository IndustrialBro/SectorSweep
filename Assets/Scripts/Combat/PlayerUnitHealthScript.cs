using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitHealthScript : HealthScript
{
    public static event Action PlayerUnitDied;
    
    int baseMaxHp;
    protected override void Start()
    {
        Reward.HAU += () =>
        {
            maxHp = baseMaxHp;
            currHp = maxHp;
        };
        Reward.IMHP += () =>
        {
            maxHp += 10;
            currHp = maxHp;
        };
        Reward.ID += () =>
        {
            maxHp = baseMaxHp;
            if(currHp > maxHp)
                currHp = maxHp;
        };
        base.Start();
        baseMaxHp = maxHp;

        PlayerUnitStrawboss.Instance.AddPlayerUnit();
    }

    protected override void Die()
    {
        PlayerUnitDied?.Invoke();
        base.Die();
    }
}
