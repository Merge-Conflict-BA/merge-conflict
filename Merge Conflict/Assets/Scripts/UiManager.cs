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

    //all menu screens
    [SerializeField] private Canvas PLAYFIELD;
    [SerializeField] private Canvas UI_MANAGER;
    [SerializeField] private Canvas MAINMENU;
    [SerializeField] private Canvas SETTINGS;
    [SerializeField] private Canvas LEVELUP;

    List<KeyValuePair<string, string>> buttonRelationScreen = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("ButtonOpenMainmenu", "Mainmenu"),
            new KeyValuePair<string, string>("ButtonCloseMainmenu", "CloseMenu"),
            new KeyValuePair<string, string>("ButtonOpenSettings", "Settings"),
            new KeyValuePair<string, string>("ButtonOpenLevelup", "Levelup"),
        };

    Canvas currentOpenedScreen;

    // Start is called before the first frame update
    void Start()
    {
        //hide all submenus at startup
        UI_MANAGER.enabled = true;
        MAINMENU.enabled = false;
        SETTINGS.enabled = false;
        LEVELUP.enabled = false;

        //setup all buttons
        setupButtonListener(buttonOpenMainmenu);
        setupButtonListener(buttonCloseMainmenu);
        setupButtonListener(buttonSettings);
        setupButtonListener(buttonLevel);
       
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
                
            case "Levelup":
                LEVELUP.enabled = true;
                currentOpenedScreen = LEVELUP;
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
