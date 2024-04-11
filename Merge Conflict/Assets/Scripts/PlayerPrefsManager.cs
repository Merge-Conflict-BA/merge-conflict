/**********************************************************************************************************************
Name:          PlayerPrefsManager
Description:   Handles some PlayerPref functions to get and set values to/from PlayerPrefs.
Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-04-09
Version:       V2.0
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour
{
    private static PlayerPrefsManager _instance;
    public static PlayerPrefsManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.W))
        {
            SaveElementsOnDeskToPlayerPrefs();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnSavedComponents();
        }
#endif
    }


    private string GetPlayerPrefsKeyWithIndex(int index)
    {
        return $"SavedComponent[{index}]";
    }

    public void SaveElementsOnDeskToPlayerPrefs(float delay = 0.2f)
    {
        StartCoroutine(SaveElementsOnDeskToPlayerPrefsAfterDelay(delay));
    }

    IEnumerator SaveElementsOnDeskToPlayerPrefsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ClearSavedElementPlayerPrefs();

        GameObject[] componentObjects = GameObject.FindGameObjectsWithTag(Tags.Component.ToString());

        int index = 0;

        foreach (GameObject componentObject in componentObjects)
        {
            componentObject.TryGetComponent(out ComponentHandler ComponentHandler);
            Element element = ComponentHandler.element;

            if (element != null)
            {
                if (ComponentHandler != null && ComponentHandler.ComponentMovement != null && ComponentHandler.ComponentMovement.IsPositionOnDesk(componentObject.GetComponent<RectTransform>().anchoredPosition))
                {
                    PlayerPrefs.SetString(GetPlayerPrefsKeyWithIndex(index), element.ToSavedElement().Serialize());
                    index++;
                }
            }
        }

        PlayerPrefs.Save();
    }

    private void ClearSavedElementPlayerPrefs()
    {

        string noComponentFound = "no component";
        int index = 0;        

        string savedComponent;
        do
        {
            string key = GetPlayerPrefsKeyWithIndex(index);

            savedComponent = PlayerPrefs.GetString(key, noComponentFound);
            PlayerPrefs.DeleteKey(key);

            index++;
        } while (savedComponent != noComponentFound);
    }

    public void SpawnSavedComponents()
    {
        List<SavedElement> savedComponents = GetSavedComponentsFromPlayerPrefs();

        foreach (SavedElement savedElement in savedComponents)
        {
            Element element = Components.GetElementByName(savedElement.Name);
            element = element.FromSavedElement(savedElement);

            element.InstantiateGameObjectAndAddTexture(ComponentSpawner.Instance.GetRandomPositionOnDesk());
        }
    }

    private List<SavedElement> GetSavedComponentsFromPlayerPrefs()
    {
        const string noComponentFound = "no component";
        string savedComponent;
        int index = 0;

        List<SavedElement> savedComponents = new();

        do
        {
            savedComponent = PlayerPrefs.GetString(GetPlayerPrefsKeyWithIndex(index), noComponentFound);
            index++;

            if (savedComponent != noComponentFound)
            {
                savedComponents.Add(SavedElement.Deserialize(savedComponent));
            }

        } while (savedComponent != noComponentFound);

        return savedComponents;
    }
}
