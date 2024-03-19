using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : Menu
{
    private static LevelMenu _instance;
    private TextMeshProUGUI _currentLevelValueTextfield;
    private TextMeshProUGUI _currentXpValueTextfield;
    private Image _levelProgressbar;
    private Canvas _levelmenuCanvas;


    public LevelMenu()
    {
        _levelmenuCanvas = FindCanvasForMenu("Level");

        InitializeMenu(_levelmenuCanvas);
        _currentLevelValueTextfield = GetComponentByName<TextMeshProUGUI>("CurrentLevelValue");
        _currentXpValueTextfield = GetComponentByName<TextMeshProUGUI>("CurrentXpValue");
        _levelProgressbar = GetComponentByName<Image>("Progressbar_empty");
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
        return _currentLevelValueTextfield.text;
    }

    public void SetDisplayedCurrentLevel(int level)
    {
        _currentLevelValueTextfield.text = level.ToString();
    }

    public string GetDisplayedCurrentXp()
    {
        return _currentXpValueTextfield.text;
    }

    public void SetDisplayedCurrentXp(int xp)
    {
        _currentXpValueTextfield.text = xp.ToString();
    }

    public void SetProgressbarValue(float level)
    {
        Transform childimageTransform = _levelProgressbar.transform.Find("Progressbar_filled");
        Image progressbar = childimageTransform.GetComponent<Image>();
        
        Debugger.LogMessage("pb: " + progressbar.name);
        double scaledLevel = level * 0.1;
        progressbar.fillAmount = (float)scaledLevel;
    }
}