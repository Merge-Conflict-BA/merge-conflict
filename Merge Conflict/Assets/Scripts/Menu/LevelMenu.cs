using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : Menu
{
    private static LevelMenu _instance;
    public static LevelMenu Instance { get { return _instance; } }
    public Canvas levelmenuCanvas; //connect to the level menu canvas
    private TextMeshProUGUI _currentLevelValueTextfield;
    const string CurrentLevelValueObjectName = "CurrentLevelValue";
    private TextMeshProUGUI _currentXpValueTextfield;
    const string CurrentXpValueObjectName = "CurrentXpValue";
    private Image _levelProgressbar;
    const string EmptyProgressbarObjectName = "Progressbar_empty";
    const string FilledProgressbarObjectName = "Progressbar_filled";

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InitializeMenu(levelmenuCanvas);
            _currentLevelValueTextfield = GetComponentbyObjectName<TextMeshProUGUI>(CurrentLevelValueObjectName);
            _currentXpValueTextfield = GetComponentbyObjectName<TextMeshProUGUI>(CurrentXpValueObjectName);
            _levelProgressbar = GetComponentbyObjectName<Image>(EmptyProgressbarObjectName);

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

    public string GetDisplayedCurrentXp()
    {
        return _currentXpValueTextfield.text;
    }

    public void SetDisplayedCurrentXp(int xp)
    {
        _currentXpValueTextfield.text = xp.ToString();
    }

    public void SetProgressbarValue(int currentXp, int xpToNextLevel)
    {
        //the progressbar is an overlay of 2 images: at the groundlayer there is an image of an 
        //empty progressbar; on top of it, there is an image with the filled progressbar from which 
        //we can control the visbile parts

        //find the GameObject which contains the image from the filled progressbar
        //if we have it, we can set the the value of the visible part of the image (=fillAmount)
        Transform filledImage = _levelProgressbar.transform.Find(FilledProgressbarObjectName);
        Image progressbar = filledImage.GetComponent<Image>();

        //normalized progressbar: 0xp => 0 | xpToNextLevel => 1
        float normalizedXp = Mathf.Clamp01(currentXp / (float)xpToNextLevel);
        progressbar.fillAmount = normalizedXp;
    }
}