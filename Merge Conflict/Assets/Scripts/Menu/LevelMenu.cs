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
    private TextMeshProUGUI _xpValueToNextLevelTextfield;
    private TextMeshProUGUI _nextLevelValueTextfield;
    private TextMeshProUGUI _caseStageValue;
    private TextMeshProUGUI _cpuStageValue;
    private TextMeshProUGUI _gpuStageValue;
    private TextMeshProUGUI _motherboardStageValue;
    private TextMeshProUGUI _ramStageValue;
    private TextMeshProUGUI _hddStageValue;
    private TextMeshProUGUI _powerSupplyStageValue;
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
            _nextLevelValueTextfield = GetComponentByName<TextMeshProUGUI>("NextLevelValue");
            _currentXpValueTextfield = GetComponentByName<TextMeshProUGUI>("CurrentXpValue");
            _xpValueToNextLevelTextfield = GetComponentByName<TextMeshProUGUI>("xpValueToNextLevel");
            _caseStageValue = GetComponentByName<TextMeshProUGUI>("CaseStageValue");
            _cpuStageValue = GetComponentByName<TextMeshProUGUI>("CpuStageValue");
            _gpuStageValue = GetComponentByName<TextMeshProUGUI>("GpuStageValue");
            _motherboardStageValue = GetComponentByName<TextMeshProUGUI>("MotherboardStageValue");
            _ramStageValue = GetComponentByName<TextMeshProUGUI>("RamStageValue");
            _hddStageValue = GetComponentByName<TextMeshProUGUI>("HddStageValue");
            _powerSupplyStageValue = GetComponentByName<TextMeshProUGUI>("PowerSupplyStageValue");
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

    public string GetDisplayeNextLevel()
    {
        return _nextLevelValueTextfield.text;
    }

    public void SetDisplayedNextLevel(int nextLevel)
    {
        _nextLevelValueTextfield.text = nextLevel.ToString();
    }

    public string GetDisplayedCurrentXp()
    {
        return _currentXpValueTextfield.text;
    }

    public void SetDisplayedCurrentXp(int xp)
    {
        _currentXpValueTextfield.text = xp.ToString();
    }

    public string GetDisplayedXpToNextLevel()
    {
        return _xpValueToNextLevelTextfield.text;
    }

    public void SetDisplayedXpToNextLevel(int maxXp)
    {
        _xpValueToNextLevelTextfield.text = maxXp.ToString();
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
        Transform childimageTransform = _levelProgressbar.transform.Find("Progressbar_filled");
        Image progressbar = childimageTransform.GetComponent<Image>();

        //normalized progressbar: 0xp => 0 | xpToNextLevel => 1
        float normalizedXp = Mathf.Clamp01(currentXp / (float)xpToNextLevel);
        progressbar.fillAmount = normalizedXp;
    }
}