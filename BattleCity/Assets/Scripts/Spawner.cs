using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject[] tanks;
    GameObject tank;
    [SerializeField] bool isPlayer;
    [SerializeField] GameObject playerTank, smallTank, fastTank, bigTank; //removed armoredtank for now
    enum tankType
    {
        smallTank, fastTank, bigTank
    };

    void Start()
    {
        if (isPlayer)
        {
            tanks = new GameObject[1] { playerTank };
        }
        else
        {
            tanks = new GameObject[3] { smallTank, fastTank, bigTank}; //removed armoredtank for now
        }
    }

    public void StartSpawning()
    {
        if (!isPlayer)
        {
            List<int> tankToSpawn = new List<int>();
            tankToSpawn.Clear();
            if (LevelManager.smallTanks > 0) tankToSpawn.Add((int)tankType.smallTank);
            if (LevelManager.fastTanks > 0) tankToSpawn.Add((int)tankType.fastTank);
            if (LevelManager.bigTanks > 0) tankToSpawn.Add((int)tankType.bigTank);
            int tankID = tankToSpawn[Random.Range(0, tankToSpawn.Count)];
            tank = Instantiate(tanks[tankID], transform.position, transform.rotation);
            if (tankID == (int)tankType.smallTank) LevelManager.smallTanks--;
            else if (tankID == (int)tankType.fastTank) LevelManager.fastTanks--;
            else if (tankID == (int)tankType.bigTank) LevelManager.bigTanks--;
        }
        else
        {
        tank = Instantiate(tanks[0], transform.position, transform.rotation);
        }
    }

    public void SpawnNextTank()
    {
        if (tank != null) tank.SetActive(true);
    }
}
