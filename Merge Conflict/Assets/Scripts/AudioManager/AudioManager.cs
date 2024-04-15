/**********************************************************************************************************************
Name:          AudioManager
Description:   Manages all sounds and background music that are played. The AudioManager is a Singleton and its 
               functions for playing sounds can be called by other scripts. However, the background music is 
               controlled by this script itself.  
Author(s):     Daniel Rittrich
Date:          2024-03-24
Version:       V1.4
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
    public Playlist bgMusicPlaylist;
    public PlayMode bgMusicPlayMode;

    [Header("Music")]
    public AudioSource backgroundAudioSource;
    public AudioSource gameAudioSource;
    public AudioSource menuAudioSource;

    [Header("Effects")]
    public AudioSource buttonClickAudioSource; 
    public AudioClip buttonClickSound;

    [Space(20)]
    public AudioSource openMenuAudioSource;
    public AudioClip openMenuSound;

    [Space(20)]
    public AudioSource closeMenuAudioSource;
    public AudioClip closeMenuSound;

    [Space(20)]
    public AudioSource exitGameAudioSource;
    public AudioClip exitGameSound;

    [Space(20)]
    public AudioSource buyUpgradeButtonAudioSource;
    public AudioClip buyUpgradeButtonSound;

    [Space(20)]
    public AudioSource buyElementButtonAudioSource;
    public AudioClip buyElementButtonSound;

    [Space(20)]
    public AudioSource levelUpAudioSource;
    public AudioClip levelUpSound;

    [Space(20)]
    public AudioSource errorAudioSource;
    public AudioClip errorSound;

    [Space(20)]
    public AudioSource questCompletedAudioSource;
    public AudioClip questCompletedSound;

    [Space(20)]
    public AudioSource mergeAudioSource;  
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
    public AudioSource componentFootstepAudioSource;
    public AudioClip componentFootstepSound;
    public float componentFootstepInterval = 0.18f;


    private Coroutine playNextBGMusicSongCoroutine;
    private Coroutine componentFootstepCoroutine;

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
        playNextBGMusicSongCoroutine = StartCoroutine(PlayNextBGMusicSong());
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (playNextBGMusicSongCoroutine != null)
            {
                StopCoroutine(playNextBGMusicSongCoroutine);
            }
            playNextBGMusicSongCoroutine = StartCoroutine(PlayNextBGMusicSong());
        }
#endif
    }

    private IEnumerator PlayNextBGMusicSong()
    {
        while (true)
        {
            var clip = GetNextBGMusicSong();

            if (clip)
            {
                backgroundAudioSource.clip = clip;
                backgroundAudioSource.Play();

                yield return new WaitForSecondsRealtime(backgroundAudioSource.clip.length);
            }

            yield return null;
        }
    }

    private AudioClip GetNextBGMusicSong()
    {
        switch (bgMusicPlayMode)
        {
            case PlayMode.Random:
                return bgMusicPlaylist.NextRandomSong();

            case PlayMode.Sequential:
                return bgMusicPlaylist.NextSong();

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

    public void PlayExitGameSound()
    {
        exitGameAudioSource.PlayOneShot(exitGameSound);
    }

    public void PlayBuyUpgradeButtonSound()
    {
        buyUpgradeButtonAudioSource.PlayOneShot(buyUpgradeButtonSound);
    }

    public void PlayBuyElementButtonSound()
    {
        buyElementButtonAudioSource.PlayOneShot(buyElementButtonSound);
    }

    public void PlayErrorSound()
    {
        errorAudioSource.PlayOneShot(errorSound);
    }

    public void PlayLevelUpSound()
    {
        levelUpAudioSource.PlayOneShot(levelUpSound);
    }

    public void PlayQuestCompletedSound()
    {
        questCompletedAudioSource.PlayOneShot(questCompletedSound);
    }

    public void StartComponentFootstepLoop()
    {
        if (componentFootstepCoroutine == null)
        {
            componentFootstepCoroutine = StartCoroutine(PlayComponentFootstepsLoop());
        }
    }

    public void StopComponentFootstepLoop()
    {
        if (componentFootstepCoroutine != null)
        {
            StopCoroutine(componentFootstepCoroutine);
            componentFootstepCoroutine = null;
        }
    }

    private IEnumerator PlayComponentFootstepsLoop()
    {
        while (true)
        {
            componentFootstepAudioSource.volume = 0.5f;
            componentFootstepAudioSource.PlayOneShot(componentFootstepSound);
            yield return new WaitForSeconds(componentFootstepInterval);
        }
    }
}