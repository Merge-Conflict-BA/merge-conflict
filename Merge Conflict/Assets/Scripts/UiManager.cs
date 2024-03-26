/**********************************************************************************************************************
Name:          UiManager
Description:   Open and close the menu playfield, settings, level, elements, upgrade.  
                Hirarchy of the menus:
                UiManager (wrapper which runs this script; contains open/close button to see them both on every sub-menu)
                |-Playfield
                |-Mainmenu (contains all buttons to get to the sub-menues)
                    |-ButtonOpenSettings
                    |-ButtonOpenLevel
                    |-ButtonOpenUpgrade
                    |-ButtonOpenElements
                |-Level
                |-Settings
                |-Upgrade
                |-Elements
                |-ButtonObenMainmenu
                |-ButtonCloseMainmenu
                |-ButtonExitGame
               
               To get the current state of the menu, call the methode: getMenuVisibility(). It returns true if the menu 
               is opened; otherwise it returns false.

Author(s):     Markus Haubold
Date:          2024-02-19
Version:       V1.1 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using ExperienceSystem;
using ExperienceSystem;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //use it as singleton
    private static UiManager _instance;
    public static UiManager Instance { get { return _instance; } }

    //default buttons to orchestrate the menu
    [SerializeField] private Button buttonOpenMainmenu;
    [SerializeField] private Button buttonCloseMainmenu;
    [SerializeField] private Button buttonOpenSettings;
    [SerializeField] private Button buttonOpenLevel;
    [SerializeField] private Button buttonOpenUpgrade;
    [SerializeField] private Button buttonOpenElements;
    [SerializeField] private Button buttonExitGame;

    //all menus
    [SerializeField] private Canvas Playfield;
    [SerializeField] private Canvas UiManagerCanvas;
    [SerializeField] private Canvas Mainmenu;
    [SerializeField] private Canvas Settings;
    [SerializeField] private Canvas Level;
    [SerializeField] private Canvas Upgrade;
    [SerializeField] private Canvas Elements;

    //mapping buttons to the menu wich they should open
    List<KeyValuePair<string, string>> readableMenuName = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ButtonOpenMainmenu", "Mainmenu"),
        new KeyValuePair<string, string>("ButtonCloseMainmenu", "CloseMenu"),
        new KeyValuePair<string, string>("ButtonOpenSettings", "Settings"),
        new KeyValuePair<string, string>("ButtonOpenLevel", "Level"),
        new KeyValuePair<string, string>("ButtonOpenUpgrade", "Upgrade"),
        new KeyValuePair<string, string>("ButtonOpenElements", "Elements"),
    };

    const Canvas NO_MENU_OPENED = null;
    const string EXIT_GAME = "ButtonExitGame";

    private Canvas currentOpenedMenu;
    public bool isMenuVisible { get; private set; }

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //set default menu states 
        UiManagerCanvas.enabled = true;
        Mainmenu.enabled = false;
        Settings.enabled = false;
        Level.enabled = false;
        Upgrade.enabled = false;
        Elements.enabled = false;

        //setup eventlisteners for all buttons
        setupButtonListener(buttonOpenMainmenu);
        setupButtonListener(buttonCloseMainmenu);
        setupButtonListener(buttonOpenSettings);
        setupButtonListener(buttonOpenLevel);
        setupButtonListener(buttonOpenUpgrade);
        setupButtonListener(buttonOpenElements);
        setupButtonListener(buttonExitGame);
    }

    private void setupButtonListener(Button button)
    {
        if (button != null)
        {
            button.onClick.AddListener(() => handleButtonClick(button.name));
        }
        else
        {
            Debug.LogError($"Button with name {button} not found. Please check if the button exists and ist linkt to the script UiManager!");
        }
    }

    private void handleButtonClick(string clickedButton)
    {
        if (clickedButton == EXIT_GAME)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
            return;
        }

        switchMenu(clickedButton);
    }

    private void switchMenu(string requestedMenu)
    {
        if (currentOpenedMenu == NO_MENU_OPENED)
        {
            currentOpenedMenu = Mainmenu;
        }

        //close current opened menu
        currentOpenedMenu.enabled = false;

        //open requested menu with usage of the mapping
        KeyValuePair<string, string> menuName = readableMenuName.Find(pair => pair.Key == requestedMenu);

        switch (menuName.Value)
        {
            case "Mainmenu":
                Mainmenu.enabled = true;
                Playfield.enabled = false;
                currentOpenedMenu = Mainmenu;
                isMenuVisible = true;
                break;

            case "Settings":
                Settings.enabled = true;
                currentOpenedMenu = Settings;
                break;

            case "Level":
                Level.enabled = true;
                currentOpenedMenu = Level;


                //#### only for testing ######
                ExperienceHandler.ResetCurrentPlayerExperience();
                ExperienceHandler.AddExperiencePoints(180);
                OrderGenerator.Instance.GenerateNewOrder(9); //TODO: move the order generation to another point, here it's only for testing the menu
                //############################

                int currentLevel = ExperienceHandler.GetCurrentLevel();
                int currentXp = ExperienceHandler.GetExperiencePoints();
                int xpToUnlockNextLevel = ExperienceHandler.NeededXpToUnlockNextLevel(currentLevel + 1);

                LevelMenu.Instance.SetDisplayedCurrentLevel(currentLevel);
                LevelMenu.Instance.SetDisplayedNextLevel(currentLevel + 1);
                LevelMenu.Instance.SetDisplayedCurrentXp(currentXp);
                LevelMenu.Instance.SetProgressbarValue(currentXp, xpToUnlockNextLevel);

                LevelMenu.Instance.SetDisplayedCaseStage(OrderGenerator.Instance.OrderedCase);
                LevelMenu.Instance.SetDisplayedCpuStage(OrderGenerator.Instance.OrderedCpu);
                LevelMenu.Instance.SetDisplayedGpuStage(OrderGenerator.Instance.OrderedGpu);
                LevelMenu.Instance.SetDisplayedMotherboardStage(OrderGenerator.Instance.OrderedMotherboard);
                LevelMenu.Instance.SetDisplayedPowerStage(OrderGenerator.Instance.OrderedPowersupply);
                LevelMenu.Instance.SetDisplayedRamStage(OrderGenerator.Instance.OrderedRam);
                LevelMenu.Instance.SetDisplayedHddStage(OrderGenerator.Instance.OrderedHdd);

                break;

            case "Upgrade":
                Upgrade.enabled = true;
                currentOpenedMenu = Upgrade;
                break;

            case "Elements":
                Elements.enabled = true;
                currentOpenedMenu = Elements;
                break;

            case "CloseMenu":
                Mainmenu.enabled = false;
                Playfield.enabled = true;
                currentOpenedMenu = null;
                isMenuVisible = false;
                break;

            default:
                Debug.LogWarning("There is no menu with the name: " + menuName.Value);
                break;
        }
    }
}


