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

public class AudioMixerLevels : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string exposedParameter;

    // TODO: sound settings should be safed in playerprefs

    public void OnSliderChange(float input)
    {
        audioMixer.SetFloat(exposedParameter, input);
    }
}
