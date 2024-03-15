using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : Menu
{
    private static LevelMenu _instance;
    private TextMeshProUGUI _levelTextfield;
    private TextMeshProUGUI _xpTextfield;
    private Image _levelProgressbar;
    private Canvas _levelmenuCanvas;


    public LevelMenu()
    {
        _levelmenuCanvas = FindCanvasForMenu("Level");

        InitializeMenu(_levelmenuCanvas);
        _levelTextfield = GetTextfieldByName("CurrentLevelValue");
        _xpTextfield = GetTextfieldByName("CurrentXpValue");
    }

    public static LevelMenu GetSingleInstance()
    {
        if (_instance == null)
        {
            _instance = new LevelMenu();
        }

        return _instance;
    }

    public string GetDisplayedCurrentLevel()
    {
        return _levelTextfield.text;
    }

    public void SetDisplayedCurrentLevel(int level)
    {
        _levelTextfield.text = level.ToString();
    }

    public string GetDisplayedCurrentXp()
    {
        return _xpTextfield.text;
    }

    public void SetDisplayedCurrentXp(int xp)
    {
        _xpTextfield.text = xp.ToString();
    }

    /*
    private void SetProgressbarValue(float level)
    {
        double scaledLevel = level * 0.1;
        _levelProgressbar.fillAmount = (float)scaledLevel;
    }
    */



}
