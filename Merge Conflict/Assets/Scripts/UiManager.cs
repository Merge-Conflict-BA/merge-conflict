using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //default buttons to orchestrate the menu
    [SerializeField] private Button buttonOpenMainmenu;
    [SerializeField] private Button buttonCloseMainmenu;
    //all buttons used in the menu
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonLevel;
    [SerializeField] private Button buttonUpgrade;
    [SerializeField] private Button buttonElements;

    //all menu screens
    [SerializeField] private Canvas PLAYFIELD;
    [SerializeField] private Canvas UI_MANAGER;
    [SerializeField] private Canvas MAINMENU;
    [SerializeField] private Canvas SETTINGS;
    [SerializeField] private Canvas LEVEL;
    [SerializeField] private Canvas UPGRADE;
    [SerializeField] private Canvas ELEMENTS;

    List<KeyValuePair<string, string>> buttonRelationScreen = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("ButtonOpenMainmenu", "Mainmenu"),
            new KeyValuePair<string, string>("ButtonCloseMainmenu", "CloseMenu"),
            new KeyValuePair<string, string>("ButtonOpenSettings", "Settings"),
            new KeyValuePair<string, string>("ButtonOpenLevel", "Level"),
            new KeyValuePair<string, string>("ButtonOpenUpgrade", "Upgrade"),
            new KeyValuePair<string, string>("ButtonOpenElements", "Elements"),
        };

    Canvas currentOpenedScreen;

    void Update() {
      Debug.Log("mainmenu: " + MAINMENU.enabled);  
      Debug.Log("settings: " + SETTINGS.enabled);  
      Debug.Log("level: " + LEVEL.enabled);  
      Debug.Log("upgrade: " + UPGRADE.enabled);  
      Debug.Log("elements: " + ELEMENTS.enabled);  
    }

    void Start()
    {
        //hide all submenus at startup
        UI_MANAGER.enabled = true;
        MAINMENU.enabled = false;
        SETTINGS.enabled = false;
        LEVEL.enabled = false;
        UPGRADE.enabled = false;
        ELEMENTS.enabled = false;

        //setup all buttons
        setupButtonListener(buttonOpenMainmenu);
        setupButtonListener(buttonCloseMainmenu);
        setupButtonListener(buttonSettings);
        setupButtonListener(buttonLevel);
        setupButtonListener(buttonUpgrade);
        setupButtonListener(buttonElements);
       
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

    // Diese Methode wird aufgerufen, wenn der Button geklickt wird
    private void handleButtonClick(string clickedButton)
    {
        screenSwitcher(clickedButton);
    }

    private void screenSwitcher(string requestedScreen)
    {
        Debug.Log("currentOpenedScreen: " + currentOpenedScreen);
        if(!currentOpenedScreen)
        {
            currentOpenedScreen = MAINMENU;
        }

        //close current opened screen
        currentOpenedScreen.enabled = false;
        Debug.Log("Close " + currentOpenedScreen.name);

        //open requested screen
        KeyValuePair<string, string> screenname = buttonRelationScreen.Find(pair => pair.Key == requestedScreen);

        switch (screenname.Value)
        {
            case "Mainmenu":
                MAINMENU.enabled = true;
                PLAYFIELD.enabled = false;
                currentOpenedScreen = MAINMENU;
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
                break;

            default:
                Debug.Log("do somthing");
                break;
        }

    }

}
