using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject[] tanks;
    GameObject tank;
    [SerializeField] bool isPlayer;
    [SerializeField] GameObject playerTank, smallTank, fastTank, bigTank; //removed armoredtank for now

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
        tank = Instantiate(tanks[Random.Range(0, tanks.Length)], transform.position, transform.rotation);
    }

    public void SpawnNextTank()
    {
        if (tank != null) tank.SetActive(true);
    }
}
