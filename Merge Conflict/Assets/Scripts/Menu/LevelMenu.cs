using TMPro;
using Unity.VisualScripting;
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
        _levelTextfield = GetComponentByName<TextMeshProUGUI>("CurrentLevelValue");
        _xpTextfield = GetComponentByName<TextMeshProUGUI>("CurrentXpValue");
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

    public void SetProgressbarValue(float level)
    {
        Transform childimageTransform = _levelProgressbar.transform.Find("Progressbar_filled");
        Image progressbar = childimageTransform.GetComponent<Image>();
        
        Debugger.LogMessage("pb: " + progressbar.name);
        double scaledLevel = level * 0.1;
        progressbar.fillAmount = (float)scaledLevel;



    }



}
