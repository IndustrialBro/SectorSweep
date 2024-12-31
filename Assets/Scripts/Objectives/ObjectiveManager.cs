using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }
    private ObjectiveManager() { }

    kaeObjective ko = new kaeObjective();
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
        ko.PrepareObjective();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
