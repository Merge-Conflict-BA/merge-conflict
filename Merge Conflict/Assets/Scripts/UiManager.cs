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

Author(s):     Markus Haubold, Hanno Witzleb
Date:          2024-03-27
Version:       V2.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections.Generic;
using ExperienceSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class UiManager : MonoBehaviour
{
    //use it as singleton
    private static UiManager _instance;
    public static UiManager Instance { get { return _instance; } }

    //default buttons to orchestrate the menu
    [Header("Menu Buttons")]
    [SerializeField] private Button _buttonOpenMainmenu;
    [SerializeField] private TextMeshProUGUI _buttonOpenMainMenuText;
    [SerializeField] private Button _buttonOpenSettings;
    [SerializeField] private Button _buttonOpenLevel;
    [SerializeField] private Button _buttonOpenUpgrade;
    [SerializeField] private Button _buttonOpenElements;
    [SerializeField] private Button _buttonExitGame;
    [SerializeField] private Button _buttonSellingStation;

    //all menus
    [Header("Menu Canvases")]
    [SerializeField] private Canvas _playfield;
    [SerializeField] private Canvas _uiManagerCanvas;
    [SerializeField] private Canvas _mainmenu;
    [SerializeField] private Canvas _settings;
    [SerializeField] private Canvas _level;
    [SerializeField] private Canvas _upgrade;
    [SerializeField] private Canvas _elements;

    [Header("Other References")]
    public GameObject[] playFieldSpriteObjects;


    //mapping buttons to the menu wich they should open
    List<KeyValuePair<string, string>> readableMenuName = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ButtonOpenMainmenu", "Mainmenu"),
        new KeyValuePair<string, string>("ButtonOpenSettings", "Settings"),
        new KeyValuePair<string, string>("ButtonOpenLevel", "Level"),
        new KeyValuePair<string, string>("ButtonOpenUpgrade", "Upgrade"),
        new KeyValuePair<string, string>("ButtonOpenElements", "Elements"),
        new KeyValuePair<string, string>("SellingStation", "SellingStation"),
    };

    const Canvas NoMenuOpened = null;
    const string ExitTheGame = "ButtonExitGame";

    private Canvas _currentOpenedMenu = NoMenuOpened;
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
        _uiManagerCanvas.enabled = true;
        CloseAllMenus();
        _playfield.enabled = true;

        //setup eventlisteners for all buttons
        SetupButtonListener(_buttonOpenMainmenu);
        SetupButtonListener(_buttonOpenSettings);
        SetupButtonListener(_buttonOpenLevel);
        SetupButtonListener(_buttonOpenUpgrade);
        SetupButtonListener(_buttonOpenElements);
        SetupButtonListener(_buttonExitGame);
        SetupButtonListener(_buttonSellingStation);
    }

    private void SetupButtonListener(Button button)
    {
        if (button != null)
        {
            button.onClick.AddListener(() => HandleButtonClick(button.name));
        }
        else
        {
            Debugger.LogError($"Button with name {button} not found. Please check if the button exists and is linked to the script UiManager!");
        }
    }

    private void HandleButtonClick(string clickedButton)
    {
        if (clickedButton == ExitTheGame)
        {
            SavedElementsManager.Instance.SaveElementsOnDeskToPlayerPrefs();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            return;
        }

        SwitchMenu(clickedButton);
    }

    private void SwitchMenu(string requestedMenu)
    {
        Canvas previousOpenedMenu = _currentOpenedMenu;

        PauseGame();
        CloseAllMenus();

        //open requested menu with usage of the mapping
        KeyValuePair<string, string> menuName = readableMenuName.Find(pair => pair.Key == requestedMenu);

        switch (menuName.Value)
        {
            case "Mainmenu":
                if (previousOpenedMenu != _mainmenu)
                {
                    OpenMenu(_mainmenu);
                    AudioManager.Instance.PlayOpenMenuSound();
                }
                else
                {
                    _playfield.enabled = true;
                    ContinueGame();
                    AudioManager.Instance.PlayCloseMenuSound();
                }
                break;

            case "Settings":
                OpenMenu(_settings);
                SettingsMenu.Instance.OpenMenu();
                AudioManager.Instance.PlayButtonClickSound();
                break;

            case "Level":
                OpenMenu(_level);
                LevelMenu.Instance.OpenMenu();
                AudioManager.Instance.PlayButtonClickSound();
                break;

            case "SellingStation":
                OpenMenu(_level);
                LevelMenu.Instance.OpenMenu();
                AudioManager.Instance.PlayOpenMenuSound();
                break;

            case "Upgrade":
                OpenMenu(_upgrade);
                UpgradeMenu.Instance.OpenMenu();
                AudioManager.Instance.PlayButtonClickSound();
                break;

            case "Elements":
                OpenMenu(_elements);
                ElementsMenu.Instance.OpenMenu();
                AudioManager.Instance.PlayButtonClickSound();
                break;

            default:
                Debugger.LogWarning("There is no menu with the name: " + menuName.Value);
                break;
        }

        HandleMenuButtonText(_currentOpenedMenu);
    }

    private void OpenMenu(Canvas menuCanvas)
    {
        menuCanvas.enabled = true;
        _currentOpenedMenu = menuCanvas;

        isMenuVisible = true;

        SetPlayFieldSpritesVisible(false);
    }

    private void CloseAllMenus()
    {
        if (_elements.enabled) // needs to be close menu separately, otherwise the collider will detect clicks and purchases can be done
        {
            ElementsMenu.Instance.CloseMenu();
        }

        _mainmenu.enabled = false;
        _playfield.enabled = false;
        _elements.enabled = false;
        _level.enabled = false;
        _upgrade.enabled = false;
        _settings.enabled = false;

        if (_currentOpenedMenu != null)
        {
            _currentOpenedMenu.enabled = false;
            _currentOpenedMenu = null;
        }

        isMenuVisible = false;

        SetPlayFieldSpritesVisible(true);
    }

    private void HandleMenuButtonText(Canvas currentOpenedMenu)
    {
        string menuText;
        if (currentOpenedMenu == _mainmenu)
        {
            menuText = "Close";
        }
        else if (currentOpenedMenu == null)
        {
            menuText = "Menu";
        }
        else
        {
            menuText = "Back";
        }

        _buttonOpenMainMenuText.text = menuText;
    }

    private void SetPlayFieldSpritesVisible(bool isVisible)
    {
        // Turns off components, else they would render above the menu.
        // cant just simply set some layer, because components layers change constantly
        // and UI (Canvas, ...) ist a different render system than SpriteRenderer for Components
        // This is the easiest method ive found.
        for (int i = 0; i < playFieldSpriteObjects.Length; i++)
        {
            playFieldSpriteObjects[i].GetComponent<SortingGroup>().sortingOrder
                = isVisible ? playFieldSpriteObjects.Length - i : 0;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
    private void ContinueGame()
    {
        Time.timeScale = 1f;
    }
}


