using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private GameManager() { }
    [SerializeField]
    ScoreBoard sb;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        sb.Show();
    }
}