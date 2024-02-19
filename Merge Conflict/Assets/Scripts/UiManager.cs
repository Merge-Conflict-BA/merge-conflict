/**********************************************************************************************************************
Name:          UiManager
Description:   Open and close the screens playfield, settings, level, elements, upgrade.  
                Hirarchy of the screens:
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
Date:          2024-02-016
Version:       V1.0 
TODO:          - maybe its possible to avoid the switch-case in the screenSwitcher()?!
               - use Singleton-Pattern?!
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //default buttons to orchestrate the menu
    [SerializeField] private Button buttonOpenMainmenu;
    [SerializeField] private Button buttonCloseMainmenu;
    [SerializeField] private Button buttonOpenSettings;
    [SerializeField] private Button buttonOpenLevel;
    [SerializeField] private Button buttonOpenUpgrade;
    [SerializeField] private Button buttonOpenElements;
    [SerializeField] private Button buttonExitGame;

    //all screens
    [SerializeField] private Canvas PLAYFIELD;
    [SerializeField] private Canvas UI_MANAGER;
    [SerializeField] private Canvas MAINMENU;
    [SerializeField] private Canvas SETTINGS;
    [SerializeField] private Canvas LEVEL;
    [SerializeField] private Canvas UPGRADE;
    [SerializeField] private Canvas ELEMENTS;

    //mapping buttons to the screens wich they should open
    List<KeyValuePair<string, string>> buttonRelationScreen = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ButtonOpenMainmenu", "Mainmenu"),
        new KeyValuePair<string, string>("ButtonCloseMainmenu", "CloseMenu"),
        new KeyValuePair<string, string>("ButtonOpenSettings", "Settings"),
        new KeyValuePair<string, string>("ButtonOpenLevel", "Level"),
        new KeyValuePair<string, string>("ButtonOpenUpgrade", "Upgrade"),
        new KeyValuePair<string, string>("ButtonOpenElements", "Elements"),
    };

    const Canvas NO_OPEN_SCREEN = null;
    const string EXIT_GAME = "ButtonExitGame";
    const bool OPENED = true;
    const bool CLOSED = false;

    private Canvas currentOpenedScreen;
    private bool menuVisibility = CLOSED;


    void Update()
    {
        Debug.Log("state: " + getMenuVisibility());
    }

    void Start()
    {
        //set default screen states 
        UI_MANAGER.enabled = true;
        MAINMENU.enabled = false;
        SETTINGS.enabled = false;
        LEVEL.enabled = false;
        UPGRADE.enabled = false;
        ELEMENTS.enabled = false;

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

        screenSwitcher(clickedButton);
    }

    private void screenSwitcher(string requestedScreen)
    {
        if (currentOpenedScreen == NO_OPEN_SCREEN)
        {
            currentOpenedScreen = MAINMENU;
        }

        //close current opened screen
        currentOpenedScreen.enabled = false;

        //open requested screen with usage of the mapping
        KeyValuePair<string, string> screenname = buttonRelationScreen.Find(pair => pair.Key == requestedScreen);

        switch (screenname.Value)
        {
            case "Mainmenu":
                MAINMENU.enabled = true;
                PLAYFIELD.enabled = false;
                currentOpenedScreen = MAINMENU;
                menuVisibility = OPENED;
                break;

            case "Settings":
                SETTINGS.enabled = true;
                currentOpenedScreen = SETTINGS;
                break;

            case "Level":
                LEVEL.enabled = true;
                currentOpenedScreen = LEVEL;
                break;

            case "Upgrade":
                UPGRADE.enabled = true;
                currentOpenedScreen = UPGRADE;
                break;

            case "Elements":
                ELEMENTS.enabled = true;
                currentOpenedScreen = ELEMENTS;
                break;

            case "CloseMenu":
                MAINMENU.enabled = false;
                PLAYFIELD.enabled = true;
                currentOpenedScreen = null;
                menuVisibility = CLOSED;
                break;

            default:
                Debug.LogWarning("No Screen to open with the name: " + screenname.Value);
                break;
        }
    }

    public bool getMenuVisibility()
    {
        return menuVisibility;
    }
}