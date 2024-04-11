/**********************************************************************************************************************
Name:          Settings
Description:   Contains all settings that the user can set individually.
Author(s):     Simeon Baumann
Date:          2024-03-19
Version:       V1.0
**********************************************************************************************************************/
using UnityEngine;


public class Settings
{
    // Properties which can be stored in PlayerPrefs
    public float MusicValue;
    public float SoundValue;
    private float _defaultValue = -17f;
    
    // Singleton
    private static Settings _instance = null;
    public static Settings Instance {
        get {
            if (_instance == null) {
                _instance = new Settings();
            }
            return _instance;
        }
    }

    private Settings()
    {
        GetDataFromPlayerPrefs();
    }

    private void GetDataFromPlayerPrefs()
    {
        MusicValue = PlayerPrefs.GetFloat(nameof(MusicValue), _defaultValue);
        SoundValue = PlayerPrefs.GetFloat(nameof(SoundValue), _defaultValue);
    }

    /// <summary>
    /// Saves a property declared in the settings in PlayerPrefs.
    /// </summary>
    /// <param name="property">The name of the property (use: nameOf())</param>
    public void SaveProperty(string property)
    {
        switch (property)
        {
            case nameof(MusicValue):
                PlayerPrefs.SetFloat(nameof(MusicValue), MusicValue);
                break;
            case nameof(SoundValue):
                PlayerPrefs.SetFloat(nameof(SoundValue), SoundValue);
                break;
            default:
                Debugger.LogMessage($"Could not find property in Settings: {property}");
                break;
        }
    }
}
