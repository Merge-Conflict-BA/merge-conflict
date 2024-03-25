/**********************************************************************************************************************
Name:          Playlist
Description:   With this a list of sounds can be created.  
Author(s):     Daniel Rittrich
Date:          2024-03-24
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Own/Playlist")]
public class Playlist : ScriptableObject
{
    public List<AudioClip> clips;

    private int lastPlayedIndex = -1;

    public AudioClip NextSong()
    {
        lastPlayedIndex = (lastPlayedIndex + 1) % clips.Count;
        return clips[lastPlayedIndex];
    }

    public AudioClip NextRandomSong()
    {
        return clips[Random.Range(0, clips.Count)];
    }
}
