using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    // The GPM handles UI elements and active power-ups as well as starting and ending scenes.

    [SerializeField] Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField] TextMeshProUGUI stageNumberText, gameOverText;
    [SerializeField] RectTransform canvas;
    GameObject[] spawnPoints, spawnPlayerPoints;
    bool tankReserveEmpty = false;
    [SerializeField] Transform tankReservePanel;
    [SerializeField] TextMeshProUGUI playerLivesText, stageNumber;
    GameObject tankImage;
    public bool isShield = false, isSpeed = false, isBullets = false;
    [SerializeField] Image ShieldPUIcon, SpeedPUIcon, BulletsPUIcon;
    void Start()
    {
        stageNumberText.text = "STAGE " + MasterTracker.stageNumber.ToString();
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        spawnPlayerPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        UpdateTankReserve();
        UpdatePlayerLives();
        UpdateStageNumber();
        StartCoroutine(StartStage());
    }

    private void Update()
    {
        if (tankReserveEmpty && GameObject.FindWithTag("Small") == null && GameObject.FindWithTag("Fast") == null && GameObject.FindWithTag("Big") == null)
        {
            MasterTracker.stageCleared = true;
            LevelCompleted();
        }
    }
    private void LevelCompleted()
    {
        tankReserveEmpty = false;
        MasterTracker.stagesCleared += 1;
        SceneManager.LoadScene("Score");
    }
    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(3);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        InvokeRepeating("SpawnEnemy", LevelManager.spawnRate, LevelManager.spawnRate);
        SpawnPlayer();
        SpawnPowerUps();
        RemovePowerUps();
    }

    public void SpawnPlayer()
    {
        if (MasterTracker.playerLives > 0) //changed from >= to >
        {
            RemovePowerUps();
            UpdatePowerUps();
            UpdatePlayerLives();
            Animator anim = spawnPlayerPoints[0].GetComponent<Animator>();
            anim.SetTrigger("Spawning");
        }
        else
        {
            UpdatePlayerLives();
            StartCoroutine(GameOver());
        }
    }

    public void SpawnEnemy()
    {
        if (LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Animator anim = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            anim.SetTrigger("Spawning");
        }
        else
        {
            CancelInvoke();
            tankReserveEmpty = true;
        }
    }
    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 120f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        MasterTracker.stageCleared = false;
        LevelCompleted();
    }

    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            //make sure y stays above 0 so it actually disappears
            blackCurtain.rectTransform.localScale = new Vector3(blackCurtain.rectTransform.localScale.x, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), blackCurtain.rectTransform.localScale.z);
            yield return null;
        }
    }

    IEnumerator RevealTopStage()
    {
        float moveTopUpMin = topCurtain.rectTransform.position.y + (canvas.rect.height / 2) + 10; // buffer of 10
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < moveTopUpMin)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 200 * Time.deltaTime, 0));
            yield return null;
        }
    }

    IEnumerator RevealBottomStage()
    {
        float moveBottomDownMin = bottomCurtain.rectTransform.position.y - (canvas.rect.height / 2) - 10;
        while (bottomCurtain.rectTransform.position.y > moveBottomDownMin)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -200 * Time.deltaTime, 0));
            yield return null;
        }
    }

    void UpdateTankReserve()
    {
        int j;
        int numberOfTanks = LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks;
        for (j = 0; j < numberOfTanks; j++)
        {
            tankImage = tankReservePanel.transform.GetChild(j).gameObject;
            tankImage.SetActive(true);
        }
    }
    public void RemoveTankReserve()
    {
        int numberOfTanks = LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks;
        tankImage = tankReservePanel.transform.GetChild(numberOfTanks).gameObject;
        tankImage.SetActive(false);
    }
    public void UpdatePlayerLives(){
        playerLivesText.text = MasterTracker.playerLives.ToString();
    }
    public void UpdateStageNumber()
    {
        stageNumber.text = MasterTracker.stageNumber.ToString();
    }

    public void UpdatePowerUps()
    {
        if (isShield)   {ShieldPUIcon.enabled = true; }
        else { ShieldPUIcon.enabled = false;}
        if (isSpeed)    {SpeedPUIcon.enabled = true; }
        else { SpeedPUIcon.enabled = false;}
        if (isBullets)  {BulletsPUIcon.enabled = true; }
        else { BulletsPUIcon.enabled = false; }
    }
    
    public void RemovePowerUps()
    {
        isShield = false;
        isSpeed = false;
        isBullets = false;
        UpdatePowerUps();
    }

    void SpawnPowerUps()
    {
        string[] powerUpTags = { "ShieldPU", "SpeedPU", "BulletPU" };
        List<GameObject> powerUpList = new List<GameObject>();
        foreach (string tag in powerUpTags)
        {
            GameObject[] powerUpsWithTag = GameObject.FindGameObjectsWithTag(tag);
            powerUpList.AddRange(powerUpsWithTag);
        }
        foreach (GameObject powerUp in powerUpList) { powerUp.GetComponent<PowerUps>().SpawnPowerUp(); }
    }
}
