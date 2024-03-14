using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenu : Menu
{
    private static LevelMenu _instance;
    public static LevelMenu Instance { get { return _instance; } }
    private TextMeshProUGUI displayedCurrentLevel;
    private TextMeshProUGUI displayedCurrentXp;

    public Canvas menuCanvas;

    public LevelMenu(Canvas menuCanvas) : base(menuCanvas)
    {
        //singleton ?!
        /*
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        } */

        displayedCurrentLevel = menuCanvas.transform.Find("CurrentLevelValue")?.GetComponent<TextMeshProUGUI>();
        displayedCurrentXp = menuCanvas.transform.Find("CurrentXpValue")?.GetComponent<TextMeshProUGUI>();

        if(displayedCurrentLevel == null) {Debugger.LogError("Can't find TextMeshProUGUI with the name CurrentLevelValue from the parent canvas Level!");};
        if(displayedCurrentXp == null) {Debugger.LogError("Can't find TextMeshProUGUI with the name CurrentLevelValue from the parent canvas Level!");};
    }


    public string GetDisplayedCurrentLevel()
    {
        return displayedCurrentLevel.text;
    }

    public void SetDisplayedCurrentLevel(int level)
    {
        displayedCurrentLevel.text = level.ToString();
    }

    public string GetDisplayedCurrentXp()
    {
        return displayedCurrentXp.text;
    }

    public void SetDisplayedCurrentXp(int xp)
    {
        displayedCurrentXp.text = xp.ToString();
    }





}
