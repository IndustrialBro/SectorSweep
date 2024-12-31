using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : HealthScript
{
    public static event Action<GameObject> EnemyDied;
    protected override void Die()
    {
        EnemyDied?.Invoke(gameObject);
        base.Die();
    }
    protected override void Start()
    {
        EnemyManager.Instance.AddEnemy(gameObject);
        base.Start();
    }
}
