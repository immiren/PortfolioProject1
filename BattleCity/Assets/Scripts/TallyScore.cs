using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TallyScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hiScoreText, stageText, playerScoreText, smallTankScoreText, fastTankScoreText, bigTankScoreText, armoredTankScoreText, smallTanksDestroyed, fastTanksDestroyed, bigTanksDestroyed, armoredTanksDestroyed, totalTanksDestroyed;
    int smallTankScore, fastTankScore, bigTankScore, armoredTankScore;
    MasterTracker masterTracker;
    int smallTankPointsWorth, fastTankPointsWorth, bigTankPointsWorth, armoredTankPointsWorth;
    [SerializeField] int lastStageNumber = 2;
    [SerializeField] Image nextTank, backTank;
    [SerializeField] TextMeshProUGUI nextText, bankText;
    bool isReadyForNext = false;
    int selection = 0;
    [SerializeField] GameObject Buttons;

    void Start()
    {
        Buttons.SetActive(false);
        masterTracker = GameObject.Find("MasterTracker").GetComponent<MasterTracker>();
        smallTankPointsWorth = masterTracker.smallTankPointsWorth;
        fastTankPointsWorth = masterTracker.fastTankPointsWorth;
        bigTankPointsWorth = masterTracker.bigTankPointsWorth;
        armoredTankPointsWorth = masterTracker.armoredTankPointsWorth;
        stageText.text = "STAGE " + MasterTracker.stageNumber;
        playerScoreText.text = MasterTracker.playerScore.ToString();
        StartCoroutine(UpdateTankPoints());
    }
    private void Update()
    {

        if (isReadyForNext)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.S))
            {
                selection = 1;
                UpdateButtons();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W))
            {
                selection = 0;
                UpdateButtons();
            }
            if (Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (selection == 0) { NextStage(); }
                if (selection == 1) { SceneManager.LoadScene("StartMenu"); }
            }
        }
    }

    IEnumerator UpdateTankPoints()
    {
        for (int i = 0; i<= MasterTracker.smallTanksDestroyed; i++)
        {
            smallTankScore = smallTankPointsWorth * i;
            smallTankScoreText.text = smallTankScore.ToString();
            smallTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i<= MasterTracker.fastTanksDestroyed; i++)
        {
            fastTankScore = fastTankPointsWorth * i;
            fastTankScoreText.text = fastTankScore.ToString();
            fastTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i<= MasterTracker.bigTanksDestroyed; i++)
        {
            bigTankScore = bigTankPointsWorth * i;
            bigTankScoreText.text = bigTankScore.ToString();
            bigTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i<= MasterTracker.armoredTanksDestroyed; i++)
        {
            armoredTankScore = armoredTankPointsWorth * i;
            armoredTankScoreText.text = armoredTankScore.ToString();
            armoredTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        MasterTracker.totalTanksDestroyed += (MasterTracker.smallTanksDestroyed + MasterTracker.fastTanksDestroyed + MasterTracker.bigTanksDestroyed + MasterTracker.armoredTanksDestroyed);
        totalTanksDestroyed.text = (MasterTracker.smallTanksDestroyed + MasterTracker.fastTanksDestroyed + MasterTracker.bigTanksDestroyed + MasterTracker.armoredTanksDestroyed).ToString();
        MasterTracker.playerScore += (smallTankScore + fastTankScore + bigTankScore + armoredTankScore);
        playerScoreText.text = MasterTracker.playerScore.ToString();
        if (MasterTracker.playerScore > MasterTracker.highScore) { MasterTracker.highScore = MasterTracker.playerScore; }
        hiScoreText.text = MasterTracker.highScore.ToString();
        yield return new WaitForSeconds(1f);
        //nextTank.enabled = true;
        //nextText.enabled = true;
        isReadyForNext = true;
        Buttons.SetActive(true);
        UpdateButtons();
    }
    void ClearStatistics()
    {
        MasterTracker.smallTanksDestroyed = 0;
        MasterTracker.fastTanksDestroyed = 0;
        MasterTracker.bigTanksDestroyed = 0;
        MasterTracker.armoredTanksDestroyed = 0;
    }
    public void NextStage()
    {
        isReadyForNext = false;
        if (MasterTracker.stageCleared)
        {
            ClearStatistics();
            if (MasterTracker.stageNumber != lastStageNumber)
            {
                SceneManager.LoadScene("Stage" + (MasterTracker.stageNumber + 1));
            }
            else
            {
                SceneManager.LoadScene("End");
            }
        }
        else
        {
            ClearStatistics();
        }
    }
    void UpdateButtons()
    {
        if (selection == 0)
        {
            nextTank.enabled = true;
            backTank.enabled = false;
        }
        else
        {
            nextTank.enabled = false;
            backTank.enabled = true;
        }
    }
}
