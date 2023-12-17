using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTracker : MonoBehaviour
{
    static MasterTracker instance=null;
    
    public static int smallTanksDestroyed, fastTanksDestroyed, bigTanksDestroyed, armoredTanksDestroyed;
    public static int totalTanksDestroyed;
    public static int stageNumber;
    public static int playerLives = 3;
    public static int playerScore = 0;
    public static bool stageCleared = false;
    public static int stagesCleared = 0;
    public static int highScore;

    [SerializeField] int smallTankPoints = 100, fastTankPoints = 200, bigTankPoints = 300, armoredTankPoints = 400;
    public int smallTankPointsWorth { get { return smallTankPoints;}}
    public int fastTankPointsWorth { get { return fastTankPoints;}}
    public int bigTankPointsWorth { get { return bigTankPoints;}}
    public int armoredTankPointsWorth { get { return armoredTankPoints;}}

    void Awake()
    {
        if(instance == null) //check for existing instance of mastertracker
        {
            DontDestroyOnLoad(gameObject);
            instance = this;    
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
