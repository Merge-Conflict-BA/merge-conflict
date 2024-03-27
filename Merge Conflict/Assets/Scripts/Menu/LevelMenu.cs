/**********************************************************************************************************************
Name:          LevelMenu
Description:   Link the given data to the GameObjects from the Canvas to display the level menu.
Author(s):     Markus Haubold
Date:          2024-03-27
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/
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
    private TextMeshProUGUI _xpRatioCurrentToNextLevel;
    private TextMeshProUGUI _nextLevelValueTextfield;
    private TextMeshProUGUI _caseTierValue;
    private TextMeshProUGUI _cpuTierValue;
    private TextMeshProUGUI _gpuTierValue;
    private TextMeshProUGUI _motherboardTierValue;
    private TextMeshProUGUI _ramTierValue;
    private TextMeshProUGUI _hddTierValue;
    private TextMeshProUGUI _powerSupplyTierValue;
    private Image _levelProgressbar;


    //images of the ordered components
    [SerializeField] private Image _orderedCaseImage;
    [SerializeField] private Image _orderedHddImage;
    [SerializeField] private Image _orderedMotherboardImage;
    [SerializeField] private Image _orderedPowersupplyImage;
    [SerializeField] private Image _orderedCpuImage;
    [SerializeField] private Image _orderedGpuImage;
    [SerializeField] private Image _orderedRamImage;


    const string EmptyProgressbarObjectName = "Progressbar_empty";
    const string FilledProgressbarObjectName = "Progressbar_filled";
    const string NextLevelValueObjectName = "NextLevelValue";
    const string XpRatioCurrentToNextLevelObjectName = "XpRatio";
    const string CaseTierValueObjectName = "CaseTierValue";
    const string CpuTierValueObjectName = "CpuTierValue";
    const string GpuTierValueObjectName = "GpuTierValue";
    const string MotherboardTierValueObjectName = "MotherboardTierValue";
    const string RamTierValueObjectName = "RamTierValue";
    const string HddTierValueObjectName = "HddTierValue";
    const string PowerSupplyTierValueObjectName = "PowerSupplyTierValue";

    const string OrderedCaseImageObjectName = "Case";
    const string OrderedCpuImageObjectName = "CPU";
    const string OrderedGpuImageObjectName = "GPU";
    const string OrderedMotherboardImageObjectName = "Motherboard";
    const string OrderedPowersupplyImageObjectName = "PowerSupply";
    const string OrderedRamImageObjectName = "RAM";
    const string OrderedHddImageObjectName = "HDD";



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
            _nextLevelValueTextfield = GetComponentbyObjectName<TextMeshProUGUI>(NextLevelValueObjectName);
            _xpRatioCurrentToNextLevel = GetComponentbyObjectName<TextMeshProUGUI>(XpRatioCurrentToNextLevelObjectName);
            _caseTierValue = GetComponentbyObjectName<TextMeshProUGUI>(CaseTierValueObjectName);
            _cpuTierValue = GetComponentbyObjectName<TextMeshProUGUI>(CpuTierValueObjectName);
            _gpuTierValue = GetComponentbyObjectName<TextMeshProUGUI>(GpuTierValueObjectName);
            _motherboardTierValue = GetComponentbyObjectName<TextMeshProUGUI>(MotherboardTierValueObjectName);
            _ramTierValue = GetComponentbyObjectName<TextMeshProUGUI>(RamTierValueObjectName);
            _hddTierValue = GetComponentbyObjectName<TextMeshProUGUI>(HddTierValueObjectName);
            _powerSupplyTierValue = GetComponentbyObjectName<TextMeshProUGUI>(PowerSupplyTierValueObjectName);
            _levelProgressbar = GetComponentbyObjectName<Image>(EmptyProgressbarObjectName);
            //ordered component images



            _orderedCaseImage = GetComponentbyObjectName<Image>(OrderedCaseImageObjectName);
            _orderedHddImage = GetComponentbyObjectName<Image>(OrderedHddImageObjectName);
            _orderedMotherboardImage = GetComponentbyObjectName<Image>(OrderedMotherboardImageObjectName);
            _orderedPowersupplyImage = GetComponentbyObjectName<Image>(OrderedPowersupplyImageObjectName);
            _orderedCpuImage = GetComponentbyObjectName<Image>(OrderedCpuImageObjectName);
            _orderedGpuImage = GetComponentbyObjectName<Image>(OrderedGpuImageObjectName);
            _orderedRamImage = GetComponentbyObjectName<Image>(OrderedRamImageObjectName);



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

    public string GetDisplayeNextLevel()
    {
        return _nextLevelValueTextfield.text;
    }

    public void SetDisplayedNextLevel(int nextLevel)
    {
        _nextLevelValueTextfield.text = nextLevel.ToString();
    }

    public string GetXpRatioCurrentToNextLevel()
    {
        return _xpRatioCurrentToNextLevel.text;
    }

    public void SetXpRatioCurrentToNextLevel(string currentXpSlashXpToNextLevel)
    {
        _xpRatioCurrentToNextLevel.text = currentXpSlashXpToNextLevel;
    }

    public void SetDisplayedCaseTierAndImage(int caseTier, Sprite orderedCaseSprite)
    {
        _caseTierValue.text = caseTier.ToString();
        _orderedCaseImage.sprite = orderedCaseSprite;
    }

    public void SetDisplayedCpuTierAndImage(int cpuTier, Sprite orderedCpuSprite)
    {
        _cpuTierValue.text = cpuTier.ToString();
        _orderedCpuImage.sprite = orderedCpuSprite;
    }

    public void SetDisplayedGpuTierAndImage(int gpuTier, Sprite orderedGpuSprite)
    {
        _gpuTierValue.text = gpuTier.ToString();
        _orderedGpuImage.sprite = orderedGpuSprite;
    }

    public void SetDisplayedMotherboardTierAndImage(int motherTier, Sprite orderedMotherboardSprite)
    {
        _motherboardTierValue.text = motherTier.ToString();
        _orderedMotherboardImage.sprite = orderedMotherboardSprite;
    }

    public void SetDisplayedRamTierAndImage(int ramTier, Sprite orderedRamSprite)
    {
        _ramTierValue.text = ramTier.ToString();
        _orderedRamImage.sprite = orderedRamSprite;
    }

    public void SetDisplayedHddTierAndImage(int hddTier, Sprite orderedHddSprite)
    {
        _hddTierValue.text = hddTier.ToString();
        _orderedHddImage.sprite = orderedHddSprite;
    }

    public void SetDisplayedPowersupplyTierAndImage(int powerTier, Sprite orderedPowersupplySprite)
    {
        _powerSupplyTierValue.text = powerTier.ToString();
        _orderedPowersupplyImage.sprite = orderedPowersupplySprite;
    }

    public void SetProgressbarValue(int currentXp, int xpToNextLevel)
    {
        /*
         * the progressbar is an overlay of 2 images: at the groundlayer there is an image of an 
         * empty progressbar; on top of it, there is an image with the filled progressbar from which 
         * we can control the visbile parts
        */

        /*
         * find the GameObject which contains the image from the filled progressbar
         * if we have it, we can set the the value of the visible part of the image (=fillAmount)
        */
        Transform filledImage = _levelProgressbar.transform.Find(FilledProgressbarObjectName);
        Image progressbar = filledImage.GetComponent<Image>();

        //normalized progressbar: 0xp => 0 | xpToNextLevel => 1
        float normalizedXp = Mathf.Clamp01(currentXp / (float)xpToNextLevel);
        progressbar.fillAmount = normalizedXp;
    }
}