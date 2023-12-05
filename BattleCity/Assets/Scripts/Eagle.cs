using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public void EagleHit()
    {
        Debug.Log("Hit!");
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        StartCoroutine(GPM.GameOver());
    }
}
