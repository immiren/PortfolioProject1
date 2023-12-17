using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq.Expressions;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, levelMenu;
    enum MainMenuOptions { Play, Levels };
    enum LevelMenuOptions { Level1, Level2, Back }; // could be improved upon surely
    enum ScreenOptions { MainMenu, LevelMenu };
    ScreenOptions screenSelection;
    MainMenuOptions mainMenuSelection;
    LevelMenuOptions levelMenuSelection;
    [SerializeField] Image playTank, levelsTank, l1Tank, l2Tank, backTank;
    void Start()
    {
        screenSelection = ScreenOptions.MainMenu;
        mainMenuSelection = MainMenuOptions.Play;
        UpdateSelection();
        UpdateScreen();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.S))
        {
            SwitchSelectionDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W))
        {
            SwitchSelectionUp();
        }
        if (Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.KeypadEnter)){
            Select();
        }
    }
    void SwitchSelectionDown()
    {
        if (screenSelection == ScreenOptions.MainMenu)
        {
            mainMenuSelection++;
            if (mainMenuSelection > MainMenuOptions.Levels) { mainMenuSelection = MainMenuOptions.Levels; }
        }
        if (screenSelection == ScreenOptions.LevelMenu)
        {
            levelMenuSelection++;
            if (levelMenuSelection > LevelMenuOptions.Back) { levelMenuSelection = LevelMenuOptions.Back; }
        }
        UpdateSelection();
    }

    void SwitchSelectionUp()
    {
        if (screenSelection == ScreenOptions.MainMenu)
        {
            mainMenuSelection--;
            if (mainMenuSelection < 0) { mainMenuSelection = MainMenuOptions.Play; }
        }
        if (screenSelection == ScreenOptions.LevelMenu)
        {
            levelMenuSelection--;
            if (levelMenuSelection < 0) { levelMenuSelection = LevelMenuOptions.Level1; }
        }
        UpdateSelection();
    }
    void Select()
    {
        if (screenSelection == ScreenOptions.MainMenu)
        {
            if (mainMenuSelection == MainMenuOptions.Play)
            {
                NewRun();
                SceneManager.LoadScene("Stage1");
            }
            if (mainMenuSelection == MainMenuOptions.Levels) {
                screenSelection = ScreenOptions.LevelMenu;
                UpdateScreen();
            }
        }
        else if (screenSelection == ScreenOptions.LevelMenu)
        {
            if (levelMenuSelection == LevelMenuOptions.Level1)
            {
                NewRun();
                SceneManager.LoadScene("Stage1");
                
            }
            if (levelMenuSelection == LevelMenuOptions.Level2) {
                NewRun();
                SceneManager.LoadScene("Stage2");
            }
            if (levelMenuSelection == LevelMenuOptions.Back) {
                screenSelection = ScreenOptions.MainMenu;
                UpdateScreen();
            }
        }
    }

    void UpdateSelection()
    {
        if (screenSelection == ScreenOptions.MainMenu)
        {
            if (mainMenuSelection == MainMenuOptions.Play)
            {
                playTank.enabled = true;
                levelsTank.enabled = false;
            }
            if (mainMenuSelection == MainMenuOptions.Levels)
            {
                playTank.enabled = false;
                levelsTank.enabled = true;
            }
        }
        if (screenSelection == ScreenOptions.LevelMenu)
        {
            if (levelMenuSelection == LevelMenuOptions.Level1)
            {
                l1Tank.enabled = true;
                l2Tank.enabled = false;
                backTank.enabled = false;
            }
            if (levelMenuSelection == LevelMenuOptions.Level2)
            {
                l1Tank.enabled = false;
                l2Tank.enabled = true;
                backTank.enabled = false;
            }
            if (levelMenuSelection == LevelMenuOptions.Back)
            {
                l1Tank.enabled = false;
                l2Tank.enabled = false;
                backTank.enabled = true;
            }
        }
    }
    void UpdateScreen()
    {
        if (screenSelection == ScreenOptions.MainMenu) 
        {
            mainMenu.SetActive(true);
            levelMenu.SetActive(false);
        }
        if (screenSelection == ScreenOptions.LevelMenu)
        {
            levelMenu.SetActive(true);
            mainMenu.SetActive(false);
            levelMenuSelection = LevelMenuOptions.Level1;
        }
        UpdateSelection();
    }

    public void NewRun()
    {
        MasterTracker.totalTanksDestroyed = 0;
        MasterTracker.playerScore = 0;
        MasterTracker.stagesCleared = 0;
    }
}
