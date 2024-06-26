/**********************************************************************************************************************
Name:          AnimationManager
Description:   Contains Methods to play Animations for the playfield. 
Author(s):     Hanno Witzleb
Date:          2024-03-22
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private static AnimationManager _instance;
    public static AnimationManager Instance { get { return _instance; } }

    /*
     * Requirements for ParticleSystemPrefabs:
     * - RectTransform with Pivot set in bottomLeft
     * - scale set to 30,30
     * - in ParticleSystem Component under "Renderer" have "Sorting Layer ID" set to "Foreground"     
     * - in ParticleSystem Component have "Play on Awake" disabled (also applies to child components)
     */
    [Header("Merge Animations")]
    public ParticleSystem addToComponentAnimation;
    public ParticleSystem[] lvlUpAnimations;

    [Header("Other Animations")]
    public ParticleSystem trashAnimation;
    public ParticleSystem sellAnimation;
    public ParticleSystem componentSpawnedOnDeskAnimation;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        Debugger.LogErrorIf(
            lvlUpAnimations.Length < 3
            || addToComponentAnimation == null
            || trashAnimation == null
            || sellAnimation == null
            || componentSpawnedOnDeskAnimation == null,
            "animation prefabs needs to be set in editor!");
    }

    public void PlayMergeAnimation(Vector2 position, Element mergedElement, Element previousElement)
    {
        ParticleSystem mergeAnimation;
        if (mergedElement.GetType() == previousElement.GetType()
            && mergedElement.HasComponents() == false
            && previousElement.HasComponents() == false)
        {
            // element levels up
            mergeAnimation = lvlUpAnimations[mergedElement.tier - 2];
            AudioManager.Instance.PlayMergeSound();
        }
        else
        {
            // element is added to other component
            mergeAnimation = addToComponentAnimation;
            AudioManager.Instance.PlayCombineComponentsSound();
        }

        PlayAnimation(mergeAnimation, position);
    }

    public void PlaySellAnimation(Vector2 position)
    {
        PlayAnimation(sellAnimation, position);
    }

    public void PlayTrashAnimation(Vector2 position)
    {
        PlayAnimation(trashAnimation, position);
    }

    public void PlayComponentSpawnedOnDeskAnimation(Vector2 position)
    {
        PlayAnimation(componentSpawnedOnDeskAnimation, position);
    }

    private void PlayAnimation(ParticleSystem particleSystem, Vector2 position)
    {
        particleSystem.GetComponent<RectTransform>().anchoredPosition = position;
        particleSystem.Play();
    }
}
