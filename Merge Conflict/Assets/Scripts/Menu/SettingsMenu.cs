/**********************************************************************************************************************
Name:          SettingsMenu
Description:   Code behind the SettingsMenu.
Author(s):     Simeon Baumann
Date:          2024-03-19
Version:       V1.0
**********************************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    private Canvas _settingsMenuCanvas;
    private Settings _settings;
    private Slider _musicSlider;
    private Slider _soundSlider;

    // Singleton
    private static SettingsMenu _instance = null;
    public static SettingsMenu Instance { get { return _instance; } }
    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    private void Start()
    {
        _settingsMenuCanvas = FindCanvasForMenu("Settings");

        InitializeMenu(_settingsMenuCanvas);

        _settings = Settings.Instance;
        _musicSlider = GetComponentbyObjectName<Slider>("SliderMusicVolume");
        _soundSlider = GetComponentbyObjectName<Slider>("SliderEffectsVolume");
    }
    
    public void SetDisplayedSettings()
    {
        _musicSlider.value = _settings.MusicValue;
        _soundSlider.value = _settings.SoundValue;
        
        _musicSlider.onValueChanged.AddListener(MusicVolumeValueChanged);
        _soundSlider.onValueChanged.AddListener(EffectsVolumeValueChanged);
    }

    // Event if SliderMusicVolume is changed (--> save new value to PlayerPrefs)
    private void MusicVolumeValueChanged(float value)
    {
        _settings.MusicValue = _musicSlider.value;
        _settings.SaveProperty(nameof(_settings.MusicValue));
    }
    
    // Event if SliderEffectsVolume is changed (--> save new value to PlayerPrefs)
    private void EffectsVolumeValueChanged(float value)
    {
        _settings.SoundValue = _soundSlider.value;
        _settings.SaveProperty(nameof(_settings.SoundValue));
    }
}