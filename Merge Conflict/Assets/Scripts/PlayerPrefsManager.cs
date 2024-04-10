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
using System.Linq;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            SaveComponentsOnDeskToPlayerPrefs();
        }
        // TODO:  Test  -> delete later
        if (Input.GetKeyDown(KeyCode.R))
        {
            ExtractComponents(jsonTestString);
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

        Debugger.LogMessage($"####################  WRITE in playerprefs  ########################");
        Debugger.LogMessage("Components On Desk: " + json);
        Debugger.LogMessage($"####################################################################");
    }




    string jsonTestString = "{\"jsonComponents\":[{\"tier\":3,\"name\":\"Case\",\"variant\":0,\"Powersupply\":{\"tier\":3,\"name\":\"PowerSupply\",\"variant\":0},\"Motherboard\":{\"tier\":3,\"name\":\"Motherboard\",\"variant\":0,\"CPU\":{\"tier\":3,\"name\":\"CPU\",\"variant\":0},\"GPU\":{\"tier\":3,\"name\":\"GPU\",\"variant\":0}}},{\"tier\":1,\"name\":\"Trash\",\"variant\":0},{\"tier\":1,\"name\":\"HDD\",\"variant\":0},{\"Motherboard\":{\"tier\":4,\"name\":\"Motherboard\",\"variant\":0,\"RAM\":{\"tier\":4,\"name\":\"RAM\",\"variant\":0},\"GPU\":{\"tier\":4,\"name\":\"GPU\",\"variant\":0}}}]}";
    /* 


        public void LoadComponentsFromJSON()
        {
            //string jsonString = PlayerPrefs.GetString("ComponentsOnDesk");
            SerializableComponents componentsWrapper = JsonUtility.FromJson<SerializableComponents>(jsonTestString);

            List<List<string>> componentDetails = new List<List<string>>();

            //foreach (var component in componentsWrapper.jsonComponents)
            foreach (var component in componentsWrapper.jsonComponents)
            {
                List<string> details = new List<string>();
                ExtractComponentDetails(component, details);
                componentDetails.Add(details);
            }


            // TODO:   delete this after
            string result = string.Join(", ", componentDetails.Select(array => $"[{string.Join(", ", array)}]"));
            Debug.Log(result);

        }


        void ExtractComponentDetails(JSONComponent component, List<string> details)
        {
            details.Add($"Name: {component.name}, Tier: {component.tier}, Variant: {component.variant}");

            if (component.powersupply != null) ExtractComponentDetails(component.powersupply, details);
            if (component.motherboard != null)
            {
                ExtractComponentDetails(component.motherboard, details);
                if (component.motherboard.cpu != null) ExtractComponentDetails(component.motherboard.cpu, details);
                if (component.motherboard.ram != null) ExtractComponentDetails(component.motherboard.ram, details);
                if (component.motherboard.gpu != null) ExtractComponentDetails(component.motherboard.gpu, details);
            }
            if (component.hdd != null) ExtractComponentDetails(component.hdd, details);
            if (component.cpu != null) ExtractComponentDetails(component.cpu, details);
            if (component.ram != null) ExtractComponentDetails(component.ram, details);
            if (component.gpu != null) ExtractComponentDetails(component.gpu, details);
        }


             // result = [[{"tier":0,"name":"Trash","variant":0}],[{"tier":3,"name":"Case","variant":0} ,{"tier":3,"name":"Motherboard","variant":0} ,{"tier":3,"name":"CPU","variant":0} ]   ];

     */



  //  TODO:    WIP

    public List<List<Dictionary<string, object>>> ExtractComponents(string jsonString)
    {
        var wrapper = JsonUtility.FromJson<SerializableComponents>(jsonString);
        var result = new List<List<Dictionary<string, object>>>();

        foreach (var component in wrapper.jsonComponents)
        {
            var componentDetails = new List<Dictionary<string, object>>();
            ExtractComponentDetails(component, componentDetails);
            result.Add(componentDetails);
        }


        ShowComponentsInConsole(result);

        return result;
    }


    void ExtractComponentDetails(JSONComponent component, List<Dictionary<string, object>> componentDetails)
    {
        // Basisinformationen des aktuellen Komponentenobjekts extrahieren
        var detail = new Dictionary<string, object>
    {
        { "name", component.name },
        { "tier", component.tier },
        { "variant", component.variant }
    };
        componentDetails.Add(detail);

        // Rekursiv alle Kinderkomponenten durchsuchen
        ExploreChild(component, "Powersupply", componentDetails);
        ExploreChild(component, "Motherboard", componentDetails);
        ExploreChild(component, "CPU", componentDetails);
        ExploreChild(component, "RAM", componentDetails);
        ExploreChild(component, "GPU", componentDetails);
        ExploreChild(component, "HDD", componentDetails);
    }

    void ExploreChild(JSONComponent parentComponent, string childName, List<Dictionary<string, object>> componentDetails)
    {
        // Reflexion nutzen, um dynamisch auf das Kindobjekt zuzugreifen
        System.Reflection.PropertyInfo propInfo = parentComponent.GetType().GetProperty(childName);
        if (propInfo != null)
        {
            JSONComponent childComponent = propInfo.GetValue(parentComponent, null) as JSONComponent;
            if (childComponent != null)
            {
                // Wenn das Kindobjekt existiert, seine Details extrahieren
                ExtractComponentDetails(childComponent, componentDetails);
            }
        }
    }

    public void ShowComponentsInConsole(List<List<Dictionary<string, object>>> componentDetails)
    {
        string result = "";

        for (int i = 0; i < componentDetails.Count; i++)
        {
            string componentInfo = "Component " + (i + 1) + ": ";
            foreach (var detail in componentDetails[i])
            {
                componentInfo += "{ ";
                foreach (var key in detail.Keys)
                {
                    componentInfo += key + ": " + detail[key] + ", ";
                }
                componentInfo = componentInfo.TrimEnd(',', ' ') + " } ";
            }
            result += componentInfo + "\n";
        }

        Debug.Log(result);
    }









    // TODO:    WIP  Instantiate
    /* 
        public void IntantiateComponentsOnDeskFromPlayerPrefs()
        {
            // ......  Loop to get all components out of PlayerPrefs

            Element element = Components.GetElementByName(_name_);

            if (element == null)
            {
                Debugger.LogError("Could not get Component from PlayerPrefs.");
                return;
            }

            element.tier = _element_from_PlayerPrefs_.tier;

            element.InstantiateGameObjectAndAddTexture(ComponentSpawner.Instance.GetRandomPositionOnDesk());


            // .......
        }
     */


}
