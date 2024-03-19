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
    public bool MusicOn;
    public bool SoundOn;
    
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
        MusicOn = IntToBool(PlayerPrefs.GetInt(nameof(MusicOn), BoolToInt(true)));
        SoundOn = IntToBool(PlayerPrefs.GetInt(nameof(SoundOn), BoolToInt(true)));
    }

    /// <summary>
    /// Saves a property declared in the settings in PlayerPrefs.
    /// </summary>
    /// <param name="property">The name of the property (use: nameOf())</param>
    public void SaveProperty(string property)
    {
        switch (property)
        {
            case nameof(MusicOn):
                PlayerPrefs.SetInt(nameof(MusicOn), BoolToInt(MusicOn));
                break;
            case nameof(SoundOn):
                PlayerPrefs.SetInt(nameof(SoundOn), BoolToInt(SoundOn));
                break;
            default:
                Debugger.LogMessage($"Could not find property in Settings: {property}");
                break;
        }
    }


    // Helper methods
    // PlayerPrefs doesn't support bool. So it has to be parsed into int.
    private bool IntToBool(int value)
    {
        return value == 1;
    }

    private int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }
}
