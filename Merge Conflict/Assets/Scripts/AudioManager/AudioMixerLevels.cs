/**********************************************************************************************************************
Name:          AudioMixerLevels
Description:   Handles the volume level of the slider in the audio settings.  
Author(s):     Daniel Rittrich
Date:          2024-03-24
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


// TODO: sound settings should be safed in playerprefs

public class AudioMixerLevels : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string exposedParameter;
    
    // The threshold value below which the sound should be switched off
    public float threshold = -40f;

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
    }
}

