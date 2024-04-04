/**********************************************************************************************************************
Name:          AudioMixerLevels
Description:   Manages the volume of the slider in the audio settings and saves the values in the player settings. 
               This script must be attached directly to a slider and the slider must be inserted into the respective 
               serialisedField in Unity-Inspector.  
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

    public AudioMixerExposedParameter exposedParameter;

    [Tooltip("The threshold value below which the sound should be switched off")]
    public float threshold = -40f;

    public AudioMixerPlayerPrefsKeys playerPrefsKey;


    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(playerPrefsKey.ToString(), 0);
        SetVolumeOnStart(savedVolume);
    }

    public void OnSliderChange(float input)
    {
        if (input < threshold)
        {
            audioMixer.SetFloat(exposedParameter.ToString(), -80f);
        }
        else
        {
            audioMixer.SetFloat(exposedParameter.ToString(), input);
        }

        PlayerPrefs.SetFloat(playerPrefsKey.ToString(), input);
    }

    private void SetVolumeOnStart(float volume)
    {
        if (volume < threshold)
        {
            audioMixer.SetFloat(exposedParameter.ToString(), -80f);
        }
        else
        {
            audioMixer.SetFloat(exposedParameter.ToString(), volume);
        }

        thisSlider.value = volume;
    }
}

