using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : Menu
{
    private static LevelMenu _instance;
    public static LevelMenu Instance { get { return _instance; } }
    public Canvas levelmenuCanvas; //connect to the level menu canvas
    private TextMeshProUGUI _currentLevelValueTextfield;
    private TextMeshProUGUI _currentXpValueTextfield;
    private TextMeshProUGUI _currentTotalXpValueTextfield;

    private TextMeshProUGUI _currentNextLevelValueTextfield;
    
    private Image _levelProgressbar;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InitializeMenu(levelmenuCanvas);
            _currentLevelValueTextfield = GetComponentByName<TextMeshProUGUI>("CurrentLevelValue");
            _currentNextLevelValueTextfield = GetComponentByName<TextMeshProUGUI>("CurrentNextLevelValue");
            _currentXpValueTextfield = GetComponentByName<TextMeshProUGUI>("CurrentXpValue");
            _currentTotalXpValueTextfield = GetComponentByName<TextMeshProUGUI>("CurrentTotalXpValue");
            _levelProgressbar = GetComponentByName<Image>("Progressbar_empty");

            _instance = this;
        }
    }

    public string GetDisplayedCurrentLevel()
    {
        return _currentLevelValueTextfield.text;
    }

    public void SetDisplayedCurrentLevel(int level)
    {
        _currentLevelValueTextfield.text = level.ToString();
    }

    public string GetDisplayedCurrentNextLevel()
    {
        return _currentNextLevelValueTextfield.text;
    }

    public void SetDisplayedCurrentNextLevel(int level)
    {
        _currentNextLevelValueTextfield.text = level.ToString();
    }

    public string GetDisplayedCurrentXp()
    {
        return _currentXpValueTextfield.text;
    }

    public void SetDisplayedCurrentXp(int xp)
    {
        _currentXpValueTextfield.text = xp.ToString();
    }

    public string GetDisplayedCurrentTotalXp()
    {
        return _currentTotalXpValueTextfield.text;
    }

    public void SetDisplayedCurrentTotalXp(int xp)
    {
        _currentTotalXpValueTextfield.text = xp.ToString();
    }

    public void SetProgressbarValue(int currentXp, int xpToNextLevel)
    {
        Transform childimageTransform = _levelProgressbar.transform.Find("Progressbar_filled");
        Image progressbar = childimageTransform.GetComponent<Image>();

        //normalized progressbar: 0xp => 0 | xpToNextLevel => 1
        float normalizedXp = Mathf.Clamp01(currentXp / (float)xpToNextLevel);
        progressbar.fillAmount = normalizedXp;
    }
}