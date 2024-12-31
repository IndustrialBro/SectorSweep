using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitGunScript : GunScript
{
    int baseDmg;
    protected override void Start()
    {
        baseDmg = dmg;
        Reward.ID += () => 
        {
            dmg += 2;
        };
        Reward.IMHP += () =>
        {
            dmg = baseDmg;
        };
        Reward.HAU += () =>
        {
            dmg = baseDmg;
        };

        base.Start();
    }
}
