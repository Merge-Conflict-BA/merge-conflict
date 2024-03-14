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
        displayedCurrentLevel = GetTextfieldByName("CurrentLevelValue");
        displayedCurrentXp = GetTextfieldByName("CurrentXpValue");
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
    
    /*
    private void SetProgressbarValue(float level)
    {
        double scaledLevel = level * 0.1;
        _levelProgressbar.fillAmount = (float)scaledLevel;
    }
    */



}
