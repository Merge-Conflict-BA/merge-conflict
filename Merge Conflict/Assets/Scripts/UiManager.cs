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

public class UiManager : MonoBehaviour
{
    //use it as singleton
    private static UiManager _instance;
    public static UiManager Instance { get { return _instance; } }

    //component sprites
    public Sprite[] caseImages = new Sprite[4];
    public Sprite[] hddImages = new Sprite[4];
    public Sprite[] motherboardImages = new Sprite[4];
    public Sprite[] powersupplyImages = new Sprite[4];
    public Sprite[] cpuImages = new Sprite[4];
    public Sprite[] gpuImages = new Sprite[4];
    public Sprite[] ramImages = new Sprite[4];

    //default buttons to orchestrate the menu
    [SerializeField] private Button _buttonOpenMainmenu;
    [SerializeField] private Button _buttonCloseMainmenu;
    [SerializeField] private Button _buttonOpenSettings;
    [SerializeField] private Button _buttonOpenLevel;
    [SerializeField] private Button _buttonOpenUpgrade;
    [SerializeField] private Button _buttonOpenElements;
    [SerializeField] private Button _buttonExitGame;

    //all menus
    [SerializeField] private Canvas _playfield;
    [SerializeField] private Canvas _uiManagerCanvas;
    [SerializeField] private Canvas _mainmenu;
    [SerializeField] private Canvas _settings;
    [SerializeField] private Canvas _level;
    [SerializeField] private Canvas _upgrade;
    [SerializeField] private Canvas _elements;


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

    const Canvas NoMenuOpened = null;
    const int OffsetTierToArrayIndex = 1;
    const string ExitTheGame = "ButtonExitGame";

    private Canvas _currentOpenedMenu;
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
        _mainmenu.enabled = false;
        _settings.enabled = false;
        _level.enabled = false;
        _upgrade.enabled = false;
        _elements.enabled = false;

        //setup eventlisteners for all buttons
        SetupButtonListener(_buttonOpenMainmenu);
        SetupButtonListener(_buttonCloseMainmenu);
        SetupButtonListener(_buttonOpenSettings);
        SetupButtonListener(_buttonOpenLevel);
        SetupButtonListener(_buttonOpenUpgrade);
        SetupButtonListener(_buttonOpenElements);
        SetupButtonListener(_buttonExitGame);
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
        if (_currentOpenedMenu == NoMenuOpened)
        {
            _currentOpenedMenu = _mainmenu;
        }

        //close current opened menu
        _currentOpenedMenu.enabled = false;

        //open requested menu with usage of the mapping
        KeyValuePair<string, string> menuName = readableMenuName.Find(pair => pair.Key == requestedMenu);

        switch (menuName.Value)
        {
            case "Mainmenu":
                _mainmenu.enabled = true;
                _playfield.enabled = false;
                _currentOpenedMenu = _mainmenu;
                isMenuVisible = true;
                break;

            case "Settings":
                _settings.enabled = true;
                _currentOpenedMenu = _settings;
                break;

            case "Level":
                _level.enabled = true;
                _currentOpenedMenu = _level;

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

                //set current Tier and image of the ordered component
                LevelMenu.Instance.SetDisplayedCaseTierAndImage(order.CaseTier, caseImages[order.CaseTier - OffsetTierToArrayIndex]);
                LevelMenu.Instance.SetDisplayedCpuTierAndImage(order.CpuTier, cpuImages[order.CpuTier - OffsetTierToArrayIndex]);
                LevelMenu.Instance.SetDisplayedGpuTierAndImage(order.GpuTier, gpuImages[order.GpuTier - OffsetTierToArrayIndex]);
                LevelMenu.Instance.SetDisplayedMotherboardTierAndImage(order.MotherboardTier, motherboardImages[order.MotherboardTier - OffsetTierToArrayIndex]);
                LevelMenu.Instance.SetDisplayedPowersupplyTierAndImage(order.PowersupplyTier, powersupplyImages[order.MotherboardTier - OffsetTierToArrayIndex]);
                LevelMenu.Instance.SetDisplayedRamTierAndImage(order.RamTier, ramImages[order.RamTier - OffsetTierToArrayIndex]);
                LevelMenu.Instance.SetDisplayedHddTierAndImage(order.HddTier, hddImages[order.HddTier - OffsetTierToArrayIndex]);

                break;

            case "Upgrade":
                _upgrade.enabled = true;
                _currentOpenedMenu = _upgrade;
                break;

            case "Elements":
                _elements.enabled = true;
                _currentOpenedMenu = _elements;
                break;

            case "CloseMenu":
                _mainmenu.enabled = false;
                _playfield.enabled = true;
                _currentOpenedMenu = null;
                isMenuVisible = false;
                break;

            default:
                Debug.LogWarning("There is no menu with the name: " + menuName.Value);
                break;
        }
    }
}


