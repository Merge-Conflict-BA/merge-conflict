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
    private Toggle _musicToggle;
    private Toggle _soundToggle;

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
        _musicToggle = GetComponentByName<Toggle>("MusicValue");
        _soundToggle = GetComponentByName<Toggle>("SoundValue");
    }
    
    public void SetDisplayedSettings()
    {
        _musicToggle.isOn = _settings.MusicOn;
        _soundToggle.isOn = _settings.SoundOn;
        
        _musicToggle.onValueChanged.AddListener(MusicToggleChanged);
        _soundToggle.onValueChanged.AddListener(SoundToggleChanged);
    }

    // Event if MusicToggle is clicked (--> save new value to PlayerPrefs)
    private void MusicToggleChanged(bool value)
    {
        _settings.MusicOn = _musicToggle.isOn;
        _settings.SaveProperty(nameof(_settings.MusicOn));
    }
    
    // Event if SoundToggle is clicked (--> save new value to PlayerPrefs)
    private void SoundToggleChanged(bool value)
    {
        _settings.SoundOn = _soundToggle.isOn;
        _settings.SaveProperty(nameof(_settings.SoundOn));
    }
}