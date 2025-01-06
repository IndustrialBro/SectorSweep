using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }
    private ObjectiveManager() { }

    public event Action AllObjectivesAchieved;

    Objective[] objectives = {new kaeObjective(), new ktObjective()};
    Queue<Objective> oQueue = new Queue<Objective>();
    [SerializeField]
    int numOfObjectives;
    private void Awake()
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
    void PrepQueue()
    {
        for(int i = 0; i < numOfObjectives; i++)
        {
            oQueue.Enqueue(objectives[UnityEngine.Random.Range(0, objectives.Length)]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextObjective()
    {
        if (oQueue.Count > 0)
            oQueue.Dequeue().PrepareObjective();
        else
        {
            AllObjectivesAchieved?.Invoke();
            GameManager.Instance.EndGame();
        }
    }
}
