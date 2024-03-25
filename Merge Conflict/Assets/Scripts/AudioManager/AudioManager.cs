/**********************************************************************************************************************
Name:          AudioManager
Description:   Manages all sounds and background music that will be played.  
Author(s):     Daniel Rittrich
Date:          2024-03-24
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [Header("Playlist BG Music")]
    public Playlist playlist;
    public PlayMode playMode;

    [Header("Music")]
    public AudioSource backgroundAudioSource;
    public AudioSource gameAudioSource;
    public AudioSource menuAudioSource;
    // TODO check if these bools are useful
    private bool backgroundMusicIsPlaying = false;
    private bool gameMusicIsPlaying = false;
    private bool menuMusicIsPlaying = false;

    [Header("Effects")]
    public AudioSource buttonAudioSource; // ?  Maybe separate AudioSources for different buttons
    public AudioClip[] buttonSounds;

    public AudioSource mergeAudioSource;  // ?  Maybe separate sounds for lvl  2, 3, 4 
    public AudioSource combineComponentsAudioSource;
    public AudioSource sellAudioSource;
    public AudioSource throwAwayAudioSource; // trash
    // ?  Maybe other sounds or environment sounds 
    /*    (conveyorbelt, component walking, component dragging, component dropping, individual sounds for upgrades, 
           open menu, close menu, component returning to desk, trash walkin on desk, ... )  */

    private AudioSource audioSource;

    private Coroutine playNextSongCoroutine;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
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

    // ! ! !  audioSource for BGMusic is not connected anymore ! ! !

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

    public void PlayButtonSound()
    {
        buttonAudioSource.PlayOneShot(buttonSounds[0]);
    }
}