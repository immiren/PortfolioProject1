using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int smallTanksITL, fastTanksITL, bigTanksITL, stageNumber;
    public static int smallTanks, fastTanks, bigTanks;
    [SerializeField] float spawnRateInThisLevel = 5;
    public static float spawnRate { get; private set; }
    private void Awake()
    {
        MasterTracker.stageNumber = stageNumber;
        smallTanks = smallTanksITL;
        fastTanks = fastTanksITL;
        bigTanks = bigTanksITL;
        spawnRate = spawnRateInThisLevel;
    }
}
