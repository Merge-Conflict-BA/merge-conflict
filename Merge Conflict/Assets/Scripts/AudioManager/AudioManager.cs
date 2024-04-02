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

    [Space(20)]
    public AudioSource openMenuAudioSource;
    public AudioClip openMenuSound;

    [Space(20)]
    public AudioSource closeMenuAudioSource;
    public AudioClip closeMenuSound;

    [Space(20)]
    public AudioSource mergeAudioSource;  // ?  Maybe separate sounds for lvl  2, 3, 4 
    public AudioClip[] mergeSound;

    [Space(20)]
    public AudioSource combineComponentsAudioSource;
    public AudioClip combineComponentsSound;

    [Space(20)]
    public AudioSource sellAudioSource;
    public AudioClip sellSound;
    public AudioClip trySellWrongComponentSound;

    [Space(20)]
    public AudioSource throwAwayAudioSource; // trash
    public AudioClip throwAwaySound;

    [Space(20)]
    public AudioSource dropComponentAudioSource;
    public AudioClip dropComponentSound;

    [Space(20)]
    public AudioSource pickUpComponentAudioSource;
    public AudioClip pickUpComponentSound;

    [Space(20)]
    public AudioSource footstepAudioSource;
    public AudioClip footstepSound;
    public float footstepInterval = 0.15f;

    // ?  Maybe other sounds or environment sounds 
    /*    (conveyorbelt, component walking, component dragging, component dropping, individual sounds for upgrades, 
           open menu, close menu, component returning to desk, trash walkin on desk, level up, close menu, quest completed,
           get component when throwing one away (upgrade), buy component, selling wrong component,  ... )  */

    // TODO: don't play dropComponent Sound when selling a component or throwing it in the trash can

    private Coroutine playNextSongCoroutine;
    private Coroutine footstepCoroutine;

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

    public void PlayTrySellWrongComponentSound()
    {
        sellAudioSource.PlayOneShot(trySellWrongComponentSound);
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




    // !    ---------  WIP  --------- 

    // Methode zum Starten der Fußschritt-Schleife
    public void StartFootstepLoop()
    {
        // Stelle sicher, dass nicht bereits eine Fußschritt-Schleife läuft
        if (footstepCoroutine == null)
        {
            footstepCoroutine = StartCoroutine(PlayFootstepsLoop());
        }
    }

    // Methode zum Stoppen der Fußschritt-Schleife
    public void StopFootstepLoop()
    {
        if (footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }
    }

    // Coroutine, die den Fußschritt-Sound in einer Schleife abspielt
    private IEnumerator PlayFootstepsLoop()
    {
        // Warte die initiale Verzögerung ab, bevor die Schleife startet
        yield return new WaitForSeconds(2.5f);

        // Wiederhole solange, bis die Coroutine explizit gestoppt wird
        while (true)
        {
            footstepAudioSource.PlayOneShot(footstepSound); // Spiele den Fußschritt-Sound ab
            yield return new WaitForSeconds(footstepInterval); // Warte das angegebene Intervall ab
        }
    }
}