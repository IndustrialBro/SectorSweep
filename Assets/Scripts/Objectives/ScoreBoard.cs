using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    TMP_Text EKText, LUText, AOText, WinText, FlagText;
    public int enemiesKilled { get; private set; } = 0;
    public int lostUnits { get; private set; } = 0;
    public int achievedObjectives { get; private set; } = 0;
    public int capturedFlags { get; private set; } = 0;
    string wonOrLost = "MISSION FAILED";
    private void Start()
    {
        EnemyHealthScript.EnemyDied += (GameObject go) => { enemiesKilled++; };
        PlayerUnitHealthScript.PlayerUnitDied += () => { lostUnits++; };
        ObjectiveManager.Instance.AllObjectivesAchieved += () => { wonOrLost = "MISSION ACCOMPLISHED"; };
        FlagScript.FlagTaken += () => { capturedFlags++; };
        gameObject.SetActive(false);
    }
    public void Show()
    {
        EKText.text = $"Threats neutralized: {enemiesKilled}";
        LUText.text = $"Units lost: {lostUnits}";
        AOText.text = $"Achieved objectives: {achievedObjectives}";
        WinText.text = wonOrLost;
        FlagText.text = $"Contraband captured: {capturedFlags}";

        gameObject.SetActive(true);
    }
}
