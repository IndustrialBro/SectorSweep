using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; protected set; }
    protected ObjectiveManager() { }

    public event Action ObjectiveAchieved;
    public event Action AllObjectivesAchieved;

    protected Objective[] objectives = { new kaeObjective(), new ktObjective(), new ctfObjective() };
    protected Queue<Objective> oQueue = new Queue<Objective>();
    [SerializeField]
    int numOfObjectives;
    [SerializeField]
    protected List<Transform> flagSpawnPoints = new List<Transform>();
    [SerializeField]
    protected GameObject flagPrafab;
    protected void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PrepQueue();
        NextObjective();
    }
    protected virtual void PrepQueue()
    {
        for(int i = 0; i < numOfObjectives; i++)
        {
            oQueue.Enqueue(objectives[UnityEngine.Random.Range(0, objectives.Length)]);
        }
    }
    public void SpawnFlag()
    {
        Instantiate(flagPrafab, flagSpawnPoints[UnityEngine.Random.Range(0, flagSpawnPoints.Count)].position, Quaternion.identity);
    }
    public void NextObjective()
    {
        ObjectiveAchieved?.Invoke();
        if (oQueue.Count > 0)
            oQueue.Dequeue().PrepareObjective();
        else
        {
            AllObjectivesAchieved?.Invoke();
            GameManager.Instance.EndGame();
        }
    }
}
