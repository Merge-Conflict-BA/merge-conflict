using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //all buttons used in the menu
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonLevel;

    //all menu screens
    [SerializeField] private  Canvas screenSettings;
    [SerializeField] private Canvas screenLevel;




    // Start is called before the first frame update
    void Start()
    {
        //hide all submenus at startup
        this.screenSettings.enabled = false;
        this.screenLevel.enabled = false;

        //setup all buttons
        setupButtonListener(this.buttonSettings);
        setupButtonListener(this.buttonLevel);
       
    }

    // Update is called once per frame
    void Update()
    {

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
        Debug.Log($"Button {clickedButton} wurde geklickt!");
    }



}
