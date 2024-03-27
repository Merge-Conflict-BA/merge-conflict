/**********************************************************************************************************************
Name:          AudioMixerLevels
Description:   Handles the volume level of the slider in the audio settings and saves the values in the playerprefs.  
Author(s):     Daniel Rittrich
Date:          2024-03-24
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerLevels : MonoBehaviour
{
    public Slider thisSlider;
    public AudioMixer audioMixer;

    [Tooltip("Must be 'volumeEffects' or 'volumeMusic' !")]
    public string exposedParameter;

    // The threshold value below which the sound should be switched off
    public float threshold = -40f;

    [Tooltip("Should be 'effectsVolume' or 'musicVolume'")]
    public string playerPrefsKey;


    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(playerPrefsKey, 0);
        SetVolumeOnStart(savedVolume);
    }

    public void OnSliderChange(float input)
    {
        if (input < threshold)
        {
            audioMixer.SetFloat(exposedParameter, -80f);
        }
        else
        {
            audioMixer.SetFloat(exposedParameter, input);
        }

        PlayerPrefs.SetFloat(playerPrefsKey, input);
    }

    private void SetVolumeOnStart(float volume)
    {
        if (volume < threshold)
        {
            audioMixer.SetFloat(exposedParameter, -80f);
        }
        else
        {
            audioMixer.SetFloat(exposedParameter, volume);
        }

        thisSlider.value = volume;
    }
}

