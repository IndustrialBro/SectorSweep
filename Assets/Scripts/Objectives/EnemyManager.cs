using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private EnemyManager(){}
    public event Action AllDead;

    List<GameObject> enemies = new List<GameObject>();
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        EnemyHealthScript.EnemyDied += (GameObject go) => { enemies.Remove(go);

            if(enemies.Count == 0)
            {
                AllDead.Invoke();
            }
        };
    }

    public void AddEnemy(GameObject go) { enemies.Add(go); }
    public int GetEnemyCount() { return enemies.Count; }
}
