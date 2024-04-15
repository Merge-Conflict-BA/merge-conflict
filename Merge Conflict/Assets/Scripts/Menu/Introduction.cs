/**********************************************************************************************************************
Name:          Introduction
Description:   Handler for the menu "Introduction"
Author(s):     Markus Haubold
Date:          2024-04-11
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using UnityEngine;

public class Introduction : MonoBehaviour
{
    private static Introduction _instance;
    public static Introduction Instance { get { return _instance; } }
    public ScreenSwipe ScreenSwipe; //link to the screen swipe object
    private int FirstScreen = 0;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    //resets the screen swiper to the first screen.
    public void ResetToFirstScreen()
    {
        ScreenSwipe.GoToScreen(FirstScreen);
    }
}