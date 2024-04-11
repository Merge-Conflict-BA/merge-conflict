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
        // TODO:  Test  -> delete later
        if (Input.GetKeyDown(KeyCode.W))
        {
            SaveComponentsOnDeskToPlayerPrefs();
        }

        // TODO:  Test  -> delete later
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnSavedComponents();
        }
    }


    private string GetPlayerPrefsKeyWithIndex(int index)
    {
        return $"SavedComponent[{index}]";
    }

    public void SaveComponentsOnDeskToPlayerPrefs(float delay = 0.2f)
    {
        StartCoroutine(SaveComponentsOnDeskToPlayerPrefsAfterDelay(delay));
    }

    IEnumerator SaveComponentsOnDeskToPlayerPrefsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ClearSavedComponentPlayerPrefs();

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
                    PlayerPrefs.SetString(GetPlayerPrefsKeyWithIndex(index), element.ToJSONElement().Serialize());
                    index++;
                }
            }
        }

        PlayerPrefs.Save();
    }

    private void ClearSavedComponentPlayerPrefs()
    {

        string noComponentFound = "no component";
        string savedComponent = "";
        int index = 0;
        string key = "";

        do
        {
            key = GetPlayerPrefsKeyWithIndex(index);
            savedComponent = PlayerPrefs.GetString(key, noComponentFound);
            PlayerPrefs.DeleteKey(key);
            index++;
        } while (savedComponent != noComponentFound);
    }


    // TODO:   repair: Spawned cases with motherboard have no childs
    public void SpawnSavedComponents()
    {
        List<JSONElement> savedComponents = GetSavedComponentsFromPlayerPrefs();

        foreach (JSONElement jsonElement in savedComponents)
        {
            Element element = Components.GetElementByName(jsonElement.Name);
            element = element.FromJSONElement(jsonElement);

            element.InstantiateGameObjectAndAddTexture(ComponentSpawner.Instance.GetRandomPositionOnDesk());
        }
    }

    private List<JSONElement> GetSavedComponentsFromPlayerPrefs()
    {
        const string noComponentFound = "no component";
        string savedComponent;
        int index = 0;

        List<JSONElement> savedComponents = new();

        do
        {
            savedComponent = PlayerPrefs.GetString(GetPlayerPrefsKeyWithIndex(index), noComponentFound);
            index++;

            if (savedComponent != noComponentFound)
            {
                savedComponents.Add(JSONElement.Deserialize(savedComponent));
            }

        } while (savedComponent != noComponentFound);

        return savedComponents;
    }
}
