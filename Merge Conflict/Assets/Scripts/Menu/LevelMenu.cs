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
using ExperienceSystem;

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
    private TextMeshProUGUI _powersupplyTierValue;
    private Image _levelProgressbar;

    //images of the ordered components
    private Image _orderedCaseImage;
    private Image _orderedHddImage;
    private Image _orderedMotherboardImage;
    private Image _orderedPowersupplyImage;
    private Image _orderedCpuImage;
    private Image _orderedGpuImage;
    private Image _orderedRamImage;

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
    const string PowerSupplyTierValueObjectName = "PowersupplyTierValue";

    const string OrderedCaseImageObjectName = "Case";
    const string OrderedCpuImageObjectName = "CPU";
    const string OrderedGpuImageObjectName = "GPU";
    const string OrderedMotherboardImageObjectName = "Motherboard";
    const string OrderedPowersupplyImageObjectName = "Powersupply";
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
            _powersupplyTierValue = GetComponentbyObjectName<TextMeshProUGUI>(PowerSupplyTierValueObjectName);
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

    public void OpenMenu()
    {
        //#### only for testing ######
        //ExperienceHandler.ResetCurrentPlayerExperience();
        //ExperienceHandler.AddExperiencePoints(30);
        //############################

        //read dat from level manager
        int currentLevel = ExperienceHandler.GetCurrentLevel();
        int currentXp = ExperienceHandler.GetExperiencePointsInCurrentLevel();
        int xpToUnlockNextLevel = ExperienceHandler.NeededXpToUnlockNextLevel(currentLevel);
        string xpRatioString = $"{currentXp} / {xpToUnlockNextLevel}";

        Order? order = OrderManager.Instance.Order;
        Debugger.LogErrorIf(order == null, "Order is null, cant populate Level Screen!!");

        //set level and xp values
        SetDisplayedCurrentLevel(currentLevel);
        SetDisplayedNextLevel(currentLevel + 1);
        SetXpRatioCurrentToNextLevel(xpRatioString);
        SetProgressbarValue(currentXp, xpToUnlockNextLevel);

        TextureAtlas textures = TextureAtlas.Instance;
        CaseComponent pc = order.PC;

        //set current Tier and image of the ordered component
        SetDisplayedCaseTierAndImage(pc.tier, textures.GetComponentTexture(pc).sprite);
        SetDisplayedHddTierAndImage(pc.hdd.tier, textures.GetComponentTexture(pc.hdd).sprite);
        SetDisplayedPowersupplyTierAndImage(pc.powersupply.tier, textures.GetComponentTexture(pc.powersupply).sprite);
        SetDisplayedMotherboardTierAndImage(pc.motherboard.tier, textures.GetComponentTexture(pc.motherboard).sprite);
        SetDisplayedCpuTierAndImage(pc.motherboard.cpu.tier, textures.GetComponentTexture(pc.motherboard.cpu).sprite);
        SetDisplayedGpuTierAndImage(pc.motherboard.gpu.tier, textures.GetComponentTexture(pc.motherboard.gpu).sprite);
        SetDisplayedRamTierAndImage(pc.motherboard.ram.tier, textures.GetComponentTexture(pc.motherboard.ram).sprite);

    }

    public string GetDisplayedCurrentLevel()
    {
        return _currentLevelValueTextfield.text;
    }

    private void SetDisplayedCurrentLevel(int level)
    {
        _currentLevelValueTextfield.text = level.ToString();
    }

    public string GetDisplayeNextLevel()
    {
        return _nextLevelValueTextfield.text;
    }

    private void SetDisplayedNextLevel(int nextLevel)
    {
        _nextLevelValueTextfield.text = nextLevel.ToString();
    }

    public string GetXpRatioCurrentToNextLevel()
    {
        return _xpRatioCurrentToNextLevel.text;
    }

    private void SetXpRatioCurrentToNextLevel(string currentXpSlashXpToNextLevel)
    {
        _xpRatioCurrentToNextLevel.text = currentXpSlashXpToNextLevel;
    }

    private void SetDisplayedCaseTierAndImage(int caseTier, Sprite orderedCaseSprite)
    {
        _caseTierValue.text = caseTier.ToString();
        _orderedCaseImage.sprite = orderedCaseSprite;
    }

    private void SetDisplayedCpuTierAndImage(int cpuTier, Sprite orderedCpuSprite)
    {
        _cpuTierValue.text = cpuTier.ToString();
        _orderedCpuImage.sprite = orderedCpuSprite;
    }

    private void SetDisplayedGpuTierAndImage(int gpuTier, Sprite orderedGpuSprite)
    {
        _gpuTierValue.text = gpuTier.ToString();
        _orderedGpuImage.sprite = orderedGpuSprite;
    }

    private void SetDisplayedMotherboardTierAndImage(int motherTier, Sprite orderedMotherboardSprite)
    {
        _motherboardTierValue.text = motherTier.ToString();
        _orderedMotherboardImage.sprite = orderedMotherboardSprite;
    }

    private void SetDisplayedRamTierAndImage(int ramTier, Sprite orderedRamSprite)
    {
        _ramTierValue.text = ramTier.ToString();
        _orderedRamImage.sprite = orderedRamSprite;
    }

    private void SetDisplayedHddTierAndImage(int hddTier, Sprite orderedHddSprite)
    {
        _hddTierValue.text = hddTier.ToString();
        _orderedHddImage.sprite = orderedHddSprite;
    }

    private void SetDisplayedPowersupplyTierAndImage(int powerTier, Sprite orderedPowersupplySprite)
    {
        _powersupplyTierValue.text = powerTier.ToString();
        _orderedPowersupplyImage.sprite = orderedPowersupplySprite;
    }

    private void SetProgressbarValue(int currentXp, int xpToNextLevel)
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