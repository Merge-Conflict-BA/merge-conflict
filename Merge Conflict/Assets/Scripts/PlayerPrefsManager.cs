/**********************************************************************************************************************
Name:          PlayerPrefsManager
Description:   Handles some PlayerPref functions to get and set values to/from PlayerPrefs.
Author(s):     Daniel Rittrich
Date:          2024-04-09
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

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






    // TODO : ---------------------------------------  WIP  -----------------------------------------------
    private void Update()
    {
        // TODO:  Test  -> delete later
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveComponentsOnDeskToPlayerPrefs();
        }
    }


    [System.Serializable]
    public class SerializableComponents
    {
        public List<JSONComponent> jsonComponents = new List<JSONComponent>();
    }

    public void SaveComponentsOnDeskToPlayerPrefs()
    {
        SerializableComponents serializableComponents = new SerializableComponents();
        GameObject[] components = GameObject.FindGameObjectsWithTag("Component");

        foreach (var component in components)
        {
            component.TryGetComponent(out ComponentHandler ComponentHandler);
            Element element = ComponentHandler.element;

            if (element != null)
            {
                if (ComponentHandler != null && ComponentHandler.ComponentMovement != null && ComponentHandler.ComponentMovement.IsPositionOnDesk(component.GetComponent<RectTransform>().anchoredPosition))
                {
                    JSONComponent jsonComponent = element.CreateJSONComponentFromElement();
                    serializableComponents.jsonComponents.Add(jsonComponent);
                }
            }
        }

        string json = JsonUtility.ToJson(serializableComponents);
        PlayerPrefs.SetString("ComponentsOnDesk", json);
        PlayerPrefs.Save();

        Debugger.LogMessage("Components On Desk: " + json);
    }



/* 
    public void IntantiateComponentsOnDeskFromPlayerPrefs()
    {
        // ......  Loop to get all components out of PlayerPrefs

        Element element = Components.GetElementByName(  _name_  );

        if (element == null)
        {
            Debugger.LogError("Could not get Component from PlayerPrefs.");
            return;
        }

        element.tier =  _element_from_PlayerPrefs_   .tier;

        element.InstantiateGameObjectAndAddTexture(ComponentSpawner.Instance.GetRandomPositionOnDesk());


        // .......
    }
 */


}
