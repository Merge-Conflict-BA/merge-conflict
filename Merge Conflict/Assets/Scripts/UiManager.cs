using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //all buttons used in the menu
    [SerializeField] private Button buttonOpenSettings;
    [SerializeField] private Button buttonOpenLevelup;

    //all menu screens
    [SerializeField] private  Canvas settings;
    [SerializeField] private Canvas levelUp;



    // Start is called before the first frame update
    void Start()
    {
        //hide all submenus at startup
        settings.enabled = false;
        levelUp.enabled = false;

        //setup all buttons
        setupButtonListener(buttonOpenSettings);
        setupButtonListener(buttonOpenLevelup);
       
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
            Debug.LogError("Button not found. Please assign a Button component to the script.");
        }
    }




    // Diese Methode wird aufgerufen, wenn der Button geklickt wird
    void handleButtonClick(string clickedButton)
    {
        Debug.Log($"Button {clickedButton} wurde geklickt!");
    }



}
