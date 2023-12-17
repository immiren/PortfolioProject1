using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScoreText, totalTanksDestroyed, stagesCleared, nextText;
    [SerializeField] Image nextTank;
    MasterTracker masterTracker;
    bool isReadyToNext = false;

void Start()
    {
        isReadyToNext = false;
        nextTank.enabled = false;
        nextText.enabled = false;
        masterTracker = GameObject.Find("MasterTracker").GetComponent<MasterTracker>();
        totalTanksDestroyed.text = MasterTracker.totalTanksDestroyed.ToString();
        playerScoreText.text = MasterTracker.playerScore.ToString();
        stagesCleared.text = MasterTracker.stagesCleared.ToString();
        StartCoroutine(ShowContinueButton());
    }

    IEnumerator ShowContinueButton()
    {
        yield return new WaitForSeconds(2f);
        nextTank.enabled = true;
        nextText.enabled = true;
        isReadyToNext = true;
    }

    private void Update()
    {
        if (isReadyToNext && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
