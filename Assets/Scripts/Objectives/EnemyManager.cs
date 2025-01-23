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

    [SerializeField]
    GameObject enemyPrefab, unitFollower;
    List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    List<Transform> spawnPoints = new List<Transform>();
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
                AllDead?.Invoke();
            }
        };
    }

    public void AddEnemy(GameObject go) { enemies.Add(go); }
    public int GetEnemyCount() { return enemies.Count; }
    public GameObject GetRandomEnemy()
    {
        GameObject e;
        if(enemies.Count > 0)
        {
            e = enemies[UnityEngine.Random.Range(0, enemies.Count)];
        }
        else
        {
            e = SpawnEnemy();
        }
        e.AddComponent<FollowedUnit>().SetUp("T", unitFollower);
        return e;
    }
    GameObject SpawnEnemy()
    {
        return Instantiate(enemyPrefab, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
    }
    public void RequestSpawnEnemy(int requestedAmmount, int maxAmmount)
    {
        if(enemies.Count + requestedAmmount < maxAmmount)
        {
            for(int i = 0; i < requestedAmmount; i++) SpawnEnemy();
        }
    }
}