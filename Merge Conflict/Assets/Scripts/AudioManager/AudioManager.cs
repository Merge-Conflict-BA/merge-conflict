/**********************************************************************************************************************
Name:          AudioManager
Description:   Manages all sounds and background music that will be played.  
Author(s):     Daniel Rittrich
Date:          2024-03-24
Version:       V1.2
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
    public AudioSource buttonClickAudioSource; // ?  Maybe separate AudioSources for different buttons
    public AudioClip buttonClickSound;

    public AudioSource openMenuAudioSource;
    public AudioClip openMenuSound;

    public AudioSource closeMenuAudioSource;
    public AudioClip closeMenuSound;

    public AudioSource mergeAudioSource;  // ?  Maybe separate sounds for lvl  2, 3, 4 
    public AudioClip[] mergeSound;

    public AudioSource combineComponentsAudioSource;
    public AudioClip combineComponentsSound;

    public AudioSource sellAudioSource;
    public AudioClip sellSound;

    public AudioSource throwAwayAudioSource; // trash
    public AudioClip throwAwaySound;

    public AudioSource dropComponentAudioSource;
    public AudioClip dropComponentSound;

    public AudioSource pickUpComponentAudioSource;
    public AudioClip pickUpComponentSound;

    // ?  Maybe other sounds or environment sounds 
    /*    (conveyorbelt, component walking, component dragging, component dropping, individual sounds for upgrades, 
           open menu, close menu, component returning to desk, trash walkin on desk, level up, close menu ... )  */

    private Coroutine playNextSongCoroutine;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);
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
                backgroundAudioSource.clip = clip;
                backgroundAudioSource.Play();

                yield return new WaitForSecondsRealtime(backgroundAudioSource.clip.length);
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

    public void PlayButtonClickSound()
    {
        buttonClickAudioSource.PlayOneShot(buttonClickSound);
    }

    public void PlayOpenMenuSound()
    {
        openMenuAudioSource.PlayOneShot(openMenuSound);
    }

    public void PlayCloseMenuSound()
    {
        closeMenuAudioSource.PlayOneShot(closeMenuSound);
    }

    public void PlayMergeSound()
    {
        mergeAudioSource.PlayOneShot(mergeSound[0]);
    }

    public void PlayCombineComponentsSound()
    {
        combineComponentsAudioSource.PlayOneShot(combineComponentsSound);
    }

    public void PlaySellSound()
    {
        sellAudioSource.PlayOneShot(sellSound);
    }

    public void PlayThrowAwaySound()
    {
        throwAwayAudioSource.PlayOneShot(throwAwaySound);
    }

    public void PlayDropComponentSound()
    {
        dropComponentAudioSource.PlayOneShot(dropComponentSound);
    }

    public void PlayPickUpComponentSound()
    {
        pickUpComponentAudioSource.PlayOneShot(pickUpComponentSound);
    }
}