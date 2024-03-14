using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenu : Menu
{
    private static LevelMenu _instance;
    public static LevelMenu Instance { get { return _instance; } }

    public int displayCurrentLevel;
    public int displayCurrentXp;
    public Canvas menuCanvas;

    public LevelMenu(Canvas menuCanvas) : base(menuCanvas)
    {
        Debugger.LogMessage("instance from: " + menuCanvas);
    }

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

}
