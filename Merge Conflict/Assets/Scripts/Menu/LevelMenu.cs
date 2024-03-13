using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenu : Menu
{

    public int displayCurrentLevel;
    public int displayCurrentXp;
    public Canvas menuCanvas;

    public LevelMenu(Canvas menuCanvas) : base(menuCanvas)
    {
        Debugger.LogMessage("instance from: " + menuCanvas);
    }

}
