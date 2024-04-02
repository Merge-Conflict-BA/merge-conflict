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
    [SerializeField] private Button buttonSellingStation;

    //all menus
    [SerializeField] private Canvas PLAYFIELD;
    [SerializeField] private Canvas UI_MANAGER;
    [SerializeField] private Canvas MAINMENU;
    [SerializeField] private Canvas SETTINGS;
    [SerializeField] private Canvas LEVEL;
    [SerializeField] private Canvas UPGRADE;
    [SerializeField] private Canvas ELEMENTS;

    //mapping buttons to the menu wich they should open
    List<KeyValuePair<string, string>> readableMenuName = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ButtonOpenMainmenu", "Mainmenu"),
        new KeyValuePair<string, string>("ButtonCloseMainmenu", "CloseMenu"),
        new KeyValuePair<string, string>("ButtonOpenSettings", "Settings"),
        new KeyValuePair<string, string>("ButtonOpenLevel", "Level"),
        new KeyValuePair<string, string>("ButtonOpenUpgrade", "Upgrade"),
        new KeyValuePair<string, string>("ButtonOpenElements", "Elements"),
        new KeyValuePair<string, string>("SellingStation", "SellingStation"),
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
        setupButtonListener(buttonSellingStation);
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
            currentOpenedMenu = MAINMENU;
        }

        //close current opened menu
        currentOpenedMenu.enabled = false;

        //open requested menu with usage of the mapping
        KeyValuePair<string, string> menuName = readableMenuName.Find(pair => pair.Key == requestedMenu);

        switch (menuName.Value)
        {
            case "Mainmenu":
                AudioManager.Instance.PlayOpenMenuSound();
                MAINMENU.enabled = true;
                PLAYFIELD.enabled = false;
                currentOpenedMenu = MAINMENU;
                isMenuVisible = true;
                break;

            case "Settings":
                AudioManager.Instance.PlayButtonClickSound();
                SETTINGS.enabled = true;
                currentOpenedMenu = SETTINGS;
                break;

            case "Level":
                AudioManager.Instance.PlayButtonClickSound();
                LEVEL.enabled = true;
                currentOpenedMenu = LEVEL;
                break;

            case "SellingStation":
                AudioManager.Instance.PlayOpenMenuSound();
                LEVEL.enabled = true;
                currentOpenedMenu = LEVEL;
                break;

            case "Upgrade":
                AudioManager.Instance.PlayButtonClickSound();
                UPGRADE.enabled = true;
                currentOpenedMenu = UPGRADE;
                break;

            case "Elements":
                AudioManager.Instance.PlayButtonClickSound();
                ELEMENTS.enabled = true;
                currentOpenedMenu = ELEMENTS;
                break;

            case "CloseMenu":
                AudioManager.Instance.PlayCloseMenuSound();
                MAINMENU.enabled = false;
                PLAYFIELD.enabled = true;
                currentOpenedMenu = null;
                isMenuVisible = false;
                break;

            default:
                Debug.LogWarning("There is no menu with the name: " + menuName.Value);
                break;
        }
    }
}