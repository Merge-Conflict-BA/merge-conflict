/**********************************************************************************************************************
Name:          SoundManager
Description:   Manages all sounds and background music that will be played.  
Author(s):     Daniel Rittrich
Date:          2024-03-24
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public Playlist playlist;
    public PlayMode playMode;

    private AudioSource audioSource;
    private static SoundManager _instance;

    private Coroutine playNextSongCoroutine;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        playNextSongCoroutine = StartCoroutine(PlayNextSong());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (playNextSongCoroutine != null)
            {
                StopCoroutine(playNextSongCoroutine);
            }
            playNextSongCoroutine = StartCoroutine(PlayNextSong());
        }
    }

    private IEnumerator PlayNextSong()
    {
        while (true)
        {
            var clip = GetNextSong();

            if (clip)
            {
                audioSource.clip = clip;
                audioSource.Play();

                yield return new WaitForSecondsRealtime(audioSource.clip.length);
            }

            yield return null;
        }
    }

    private AudioClip GetNextSong()
    {
        switch (playMode)
        {
            case PlayMode.Random:
                return playlist.NextRandomSong();

            case PlayMode.Sequential:
                return playlist.NextSong();

            default:
                return null;
        }
    }
}