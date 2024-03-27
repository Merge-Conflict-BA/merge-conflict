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
    private TextMeshProUGUI _caseStageValue;
    private TextMeshProUGUI _cpuStageValue;
    private TextMeshProUGUI _gpuStageValue;
    private TextMeshProUGUI _motherboardStageValue;
    private TextMeshProUGUI _ramStageValue;
    private TextMeshProUGUI _hddStageValue;
    private TextMeshProUGUI _powerSupplyStageValue;
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
    const string CaseStageValueObjectName = "CaseStageValue";
    const string CpuStageValueObjectName = "CpuStageValue";
    const string GpuStageValueObjectName = "GpuStageValue";
    const string MotherboardStageValueObjectName = "MotherboardStageValue";
    const string RamStageValueObjectName = "RamStageValue";
    const string HddStageValueObjectName = "HddStageValue";
    const string PowerSupplyStageValueObjectName = "PowerSupplyStageValue";

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
            _caseStageValue = GetComponentbyObjectName<TextMeshProUGUI>(CaseStageValueObjectName);
            _cpuStageValue = GetComponentbyObjectName<TextMeshProUGUI>(CpuStageValueObjectName);
            _gpuStageValue = GetComponentbyObjectName<TextMeshProUGUI>(GpuStageValueObjectName);
            _motherboardStageValue = GetComponentbyObjectName<TextMeshProUGUI>(MotherboardStageValueObjectName);
            _ramStageValue = GetComponentbyObjectName<TextMeshProUGUI>(RamStageValueObjectName);
            _hddStageValue = GetComponentbyObjectName<TextMeshProUGUI>(HddStageValueObjectName);
            _powerSupplyStageValue = GetComponentbyObjectName<TextMeshProUGUI>(PowerSupplyStageValueObjectName);
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

    public string GetDisplayedCaseStage()
    {
        return _caseStageValue.text;
    }

    public void SetDisplayedCaseStage(int caseStage)
    {
        _caseStageValue.text = caseStage.ToString();
    }

    public string GetDisplayedCpuStage()
    {
        return _cpuStageValue.text;
    }

    public void SetDisplayedCpuStage(int cpuStage)
    {
        _cpuStageValue.text = cpuStage.ToString();
    }

    public string GetDisplayedGpuStage()
    {
        return _gpuStageValue.text;
    }

    public void SetDisplayedGpuStage(int gpuStage)
    {
        _gpuStageValue.text = gpuStage.ToString();
    }

    public string GetDisplayedMotherboardStage()
    {
        return _motherboardStageValue.text;
    }

    public void SetDisplayedMotherboardStage(int motherStage)
    {
        _motherboardStageValue.text = motherStage.ToString();
    }

    public string GetDisplayedRamStage()
    {
        return _ramStageValue.text;
    }

    public void SetDisplayedRamStage(int ramStage)
    {
        _ramStageValue.text = ramStage.ToString();
    }

    public string GetDisplayedHddStage()
    {
        return _hddStageValue.text;
    }

    public void SetDisplayedHddStage(int hddStage)
    {
        _hddStageValue.text = hddStage.ToString();
    }

    public string GetDisplayedPowerStage()
    {
        return _powerSupplyStageValue.text;
    }

    public void SetDisplayedPowerStage(int powerStage)
    {
        _powerSupplyStageValue.text = powerStage.ToString();
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

    public void SetOrderedCaseImage(Sprite orderdCaseImage)
    {
        Debugger.LogMessage("case image: " + orderdCaseImage.name);
        _orderedCaseImage.sprite = orderdCaseImage;
    }

    public void SetOrderedHddImage(Sprite orderdHddImage)
    {
        _orderedHddImage.sprite = orderdHddImage;
    }

    public void SetOrderedMotherboardImage(Sprite orderdMotherboardImage)
    {
        _orderedMotherboardImage.sprite = orderdMotherboardImage;
    }

    public void SetOrderedPowersupplyImage(Sprite orderdPowersupplyImage)
    {
        _orderedPowersupplyImage.sprite = orderdPowersupplyImage;
    }

    public void SetOrderedCpuImage(Sprite orderdCpuImage)
    {
        _orderedCpuImage.sprite = orderdCpuImage;
    }

    public void SetOrderedGpuImage(Sprite orderdGpuImage)
    {
        _orderedGpuImage.sprite = orderdGpuImage;
    }

    public void SetOrderedRamImage(Sprite orderdRamImage)
    {
        _orderedRamImage.sprite = orderdRamImage;
    }
}