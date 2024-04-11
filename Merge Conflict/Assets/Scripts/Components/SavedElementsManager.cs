/**********************************************************************************************************************
Name:          SavedElementsManager
Description:   Handles some SavedElements functions to get and set values to/from PlayerPrefs.
Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-04-09
Version:       V2.0
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class SavedElementsManager : MonoBehaviour
{
    private static SavedElementsManager _instance;
    public static SavedElementsManager Instance { get { return _instance; } }

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
            SpawnSavedElements();
        }
#endif
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

            if(element == null
                || ComponentHandler == null
                || ComponentHandler.ComponentMovement== null
                || ComponentHandler.ComponentMovement.IsComponentOnDesk() == false)
            {
                continue;
            }

            PlayerPrefs.SetString(GetPlayerPrefsKeyWithIndex(index), element.ToSavedElement().Serialize());
            index++;
        }

        PlayerPrefs.Save();
    }

    private void ClearSavedElementPlayerPrefs()
    {
        const string noElementFound = "no element";
        int index = 0;        

        string savedElement;
        do
        {
            string key = GetPlayerPrefsKeyWithIndex(index);

            savedElement = PlayerPrefs.GetString(key, noElementFound);
            PlayerPrefs.DeleteKey(key);

            index++;
        } while (savedElement != noElementFound);
    }

    public void SpawnSavedElements()
    {
        List<SavedElement> savedElements = GetSavedElementsFromPlayerPrefs();

        foreach (SavedElement savedElement in savedElements)
        {
            Element element = Components.GetElementByName(savedElement.Name);
            element = element.FromSavedElement(savedElement);

            element.InstantiateGameObjectAndAddTexture(ComponentSpawner.Instance.GetRandomPositionOnDesk());
        }
    }

    private List<SavedElement> GetSavedElementsFromPlayerPrefs()
    {
        const string noElementFound = "no element";
        string savedElement;
        int index = 0;

        List<SavedElement> savedElements = new();

        do
        {
            savedElement = PlayerPrefs.GetString(GetPlayerPrefsKeyWithIndex(index), noElementFound);
            index++;

            if (savedElement == noElementFound)
            {
                continue;
            }
            
            savedElements.Add(SavedElement.Deserialize(savedElement));            

        } while (savedElement != noElementFound);

        return savedElements;
    }

    private string GetPlayerPrefsKeyWithIndex(int index)
    {
        return $"SavedElement [{index}]";
    }

}
