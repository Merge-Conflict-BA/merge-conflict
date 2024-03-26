/**********************************************************************************************************************
Name:          GameState
Description:   Saves and Gets all of the found Elements to/from the PlayerPrefs. 

Author(s):     Simeon Baumann
Date:          2024-03-23
Version:       V1.0
**********************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoundElementsHandler : MonoBehaviour
{
    #region Singleton

    private static FoundElementsHandler _instance;
    public static FoundElementsHandler Instance => _instance;

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

    #endregion

    private List<FoundElement> _foundElements;
    
    void Start()
    {
        _foundElements = GetListFromPlayerPrefs();
    }

    /// <summary>
    /// Adds element to list of FoundElements if the given element is new and not been found yet.
    /// </summary>
    /// <param name="element">Element that is to be checked</param>
    public void ElementIsFound(Element element)
    {
        FoundElement foundElement = element switch
        {
            CaseComponent => new FoundElement(ElementName.Case, element.level),
            PowersupplyComponent => new FoundElement(ElementName.PowerSupply, element.level),
            HDDComponent => new FoundElement(ElementName.HDD, element.level),
            MBComponent => new FoundElement(ElementName.Motherboard, element.level),
            CPUComponent => new FoundElement(ElementName.CPU, element.level),
            RAMComponent => new FoundElement(ElementName.RAM, element.level),
            GPUComponent => new FoundElement(ElementName.GPU, element.level),
            Trash => new FoundElement(ElementName.Default, 1),
            _ => new FoundElement(ElementName.Default, 1)
        };

        if (foundElement.ElementName == ElementName.Default)
        {
            return;
        }

        // Check if the foundElement is already in the list of FoundElements
        var isNotFoundYet = true;
        foreach (var savedFoundElement in _foundElements)
        {
            if (savedFoundElement.ElementName == foundElement.ElementName && savedFoundElement.Level == foundElement.Level)
            {
                isNotFoundYet = false;
            }
        }

        // Add element to List of FoundElements if it is not been found yet
        if (isNotFoundYet)
        {
            AddFoundElement(foundElement);
        }
    }
    
    /// <summary>
    /// Returns the List of all FoundElements
    /// </summary>
    public List<FoundElement> GetFoundElements()
    {
        return _foundElements;
    }
    
    /// <summary>
    /// Updates the saved item (current saved item should be overwritten -> CountPurchased can be updated)
    /// </summary>
    public void UpdateStoredElement(FoundElement element)
    {
        SaveFoundElementToPlayerPrefs(element);
    }

    private void AddFoundElement(FoundElement element)
    {
        // Add new element to list and saves it to the PlayerPrefs
        _foundElements.Add(element);
        SaveFoundElementToPlayerPrefs(element);
    }

    /// <summary>
    /// Returns the list of all FoundElements which are stored into the PlayerPrefs
    /// </summary>
    /// <returns></returns>
    private List<FoundElement> GetListFromPlayerPrefs()
    {
        List<FoundElement> foundElementsTemp = new List<FoundElement>();
        List<ElementName> elementNames = Enum.GetValues(typeof(ElementName)).Cast<ElementName>().ToList();

        foreach (var elementName in elementNames)
        {
            for (int level = 1; level < 4; level++)
            {
                string key = GetPlayerPrefsKey(new FoundElement(elementName, level));
                if (PlayerPrefs.HasKey(key))
                {
                    int countPurchased = PlayerPrefs.GetInt(key);
                    foundElementsTemp.Add(new FoundElement(elementName, level, countPurchased));
                }
            }
        }

        return foundElementsTemp;
    }

    private void SaveFoundElementToPlayerPrefs(FoundElement element)
    {
        string key = GetPlayerPrefsKey(element);
        PlayerPrefs.SetInt(key, element.CountPurchased);
    }

    private string GetPlayerPrefsKey(FoundElement element)
    {
        return $"{element.ElementName}_{element.Level}";
    }
}