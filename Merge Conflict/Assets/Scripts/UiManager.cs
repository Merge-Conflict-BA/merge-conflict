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
Date:          2024-03-27
Version:       V2.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections.Generic;
using ExperienceSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    //use it as singleton
    private static UiManager _instance;
    public static UiManager Instance { get { return _instance; } }

    //default buttons to orchestrate the menu
    [SerializeField] private Button _buttonOpenMainmenu;
    [SerializeField] private TextMeshProUGUI _buttonOpenMainMenuText;
    [SerializeField] private Button _buttonOpenSettings;
    [SerializeField] private Button _buttonOpenLevel;
    [SerializeField] private Button _buttonOpenUpgrade;
    [SerializeField] private Button _buttonOpenElements;
    [SerializeField] private Button _buttonExitGame;
    [SerializeField] private Button _buttonSellingStation;
    [SerializeField] private Button _buttonOpenIntroduction;

    //all menus
    [SerializeField] private Canvas _playfield;
    [SerializeField] private Canvas _uiManagerCanvas;
    [SerializeField] private Canvas _mainmenu;
    [SerializeField] private Canvas _settings;
    [SerializeField] private Canvas _level;
    [SerializeField] private Canvas _upgrade;
    [SerializeField] private Canvas _elements;
    [SerializeField] private Canvas _introduction;


    //mapping buttons to the menu wich they should open
    List<KeyValuePair<string, string>> readableMenuName = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ButtonOpenMainmenu", "Mainmenu"),
        new KeyValuePair<string, string>("ButtonOpenSettings", "Settings"),
        new KeyValuePair<string, string>("ButtonOpenLevel", "Level"),
        new KeyValuePair<string, string>("ButtonOpenUpgrade", "Upgrade"),
        new KeyValuePair<string, string>("ButtonOpenElements", "Elements"),
        new KeyValuePair<string, string>("SellingStation", "Level"),
        new KeyValuePair<string, string>("ButtonOpenIntroduction", "Introduction"),
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
        SetupButtonListener(_buttonOpenIntroduction);
    }

    private void SetupButtonListener(Button button)
    {
        if (button != null)
        {
            button.onClick.AddListener(() => HandleButtonClick(button.name));
        }
        else
        {
            Debug.LogError($"Button with name {button} not found. Please check if the button exists and is linked to the script UiManager!");
        }
    }

    private void HandleButtonClick(string clickedButton)
    {
        if (clickedButton == ExitTheGame)
        {
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
                }
                else
                {
                    _playfield.enabled = true;
                    ContinueGame();
                }
                break;

            case "Settings":
                OpenMenu(_settings);
                break;

            case "Level":
                OpenMenu(_level);

                //#### only for testing ######
                ExperienceHandler.ResetCurrentPlayerExperience();
                ExperienceHandler.AddExperiencePoints(30);
                //############################

                //read dat from level manager
                int currentLevel = 9;// ExperienceHandler.GetCurrentLevel();
                int currentXp = ExperienceHandler.GetExperiencePoints();
                int xpToUnlockNextLevel = ExperienceHandler.NeededXpToUnlockNextLevel(currentLevel);
                string xpRatioString = $"{currentXp} / {xpToUnlockNextLevel}";

                //#### only for testing ######
                Order order = OrderGenerator.Instance.GenerateNewOrder(currentLevel); //TODO: move the order generation to another point, here it's only for testing the menu
                //############################

                //set level and xp values
                LevelMenu.Instance.SetDisplayedCurrentLevel(currentLevel);
                LevelMenu.Instance.SetDisplayedNextLevel(currentLevel + 1);
                LevelMenu.Instance.SetXpRatioCurrentToNextLevel(xpRatioString);
                LevelMenu.Instance.SetProgressbarValue(currentXp, xpToUnlockNextLevel);

                TextureAtlas textures = TextureAtlas.Instance;

                //set current Tier and image of the ordered component
                LevelMenu.Instance.SetDisplayedCaseTierAndImage(order.CaseTier, textures.GetComponentTexture(order.PC).sprite);
                LevelMenu.Instance.SetDisplayedHddTierAndImage(order.HddTier, textures.GetComponentTexture(order.PC.hdd).sprite);
                LevelMenu.Instance.SetDisplayedPowersupplyTierAndImage(order.PowersupplyTier, textures.GetComponentTexture(order.PC.powersupply).sprite);
                LevelMenu.Instance.SetDisplayedMotherboardTierAndImage(order.MotherboardTier, textures.GetComponentTexture(order.PC.motherboard).sprite);
                LevelMenu.Instance.SetDisplayedCpuTierAndImage(order.CpuTier, textures.GetComponentTexture(order.PC.motherboard.cpu).sprite);
                LevelMenu.Instance.SetDisplayedGpuTierAndImage(order.GpuTier, textures.GetComponentTexture(order.PC.motherboard.gpu).sprite);
                LevelMenu.Instance.SetDisplayedRamTierAndImage(order.RamTier, textures.GetComponentTexture(order.PC.motherboard.ram).sprite);

                break;

            case "Upgrade":
                OpenMenu(_upgrade);
                break;

            case "Elements":
                OpenMenu(_elements);
                ElementsMenu.Instance.OpenMenu();
                break;

            case "Introduction":
                _introduction.enabled = true;
                _currentOpenedMenu = _introduction;
                break;

            case "CloseMenu":
                _mainmenu.enabled = false;
                _playfield.enabled = true;
                _elements.enabled = false;
                _currentOpenedMenu = null;
                isMenuVisible = false;
                break;

            default:
                Debug.LogWarning("There is no menu with the name: " + menuName.Value);
                break;
        }

        HandleMenuButtonText(_currentOpenedMenu);
    }

    private void OpenMenu(Canvas menuCanvas)
    {
        menuCanvas.enabled = true;
        _currentOpenedMenu = menuCanvas;

        isMenuVisible = true;
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
        _introduction.enabled = false;
        

        if (_currentOpenedMenu != null)
        {
            _currentOpenedMenu.enabled = false;
            _currentOpenedMenu = null;
        }

        isMenuVisible = false;
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

    private void PauseGame()
    {
        //Time.timeScale = 0f;
    }
    private void ContinueGame()
    {
        //Time.timeScale = 1f; 
    }
}


