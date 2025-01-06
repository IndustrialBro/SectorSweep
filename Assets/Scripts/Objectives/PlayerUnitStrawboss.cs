using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerUnitStrawboss : MonoBehaviour
{
    public static PlayerUnitStrawboss Instance { get; private set; }
    private PlayerUnitStrawboss() { }
    int livingUnits = 0;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        PlayerUnitHealthScript.PlayerUnitDied += () => 
        { 
            livingUnits--;

            if(livingUnits == 0)
                GameManager.Instance.EndGame();
        };
    }
    public void AddPlayerUnit()
    {
        livingUnits++;
    }
}
