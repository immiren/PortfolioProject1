using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScoreText, totalTanksDestroyed, stagesCleared;
    MasterTracker masterTracker;
    void Start()
    {
        masterTracker = GameObject.Find("MasterTracker").GetComponent<MasterTracker>();
        totalTanksDestroyed.text = (MasterTracker.smallTanksDestroyed + MasterTracker.fastTanksDestroyed + MasterTracker.bigTanksDestroyed + MasterTracker.armoredTanksDestroyed).ToString();
        playerScoreText.text = MasterTracker.playerScore.ToString();
        stagesCleared.text = MasterTracker.stageNumber.ToString();
    }
}
