using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region Singleton

    private static GameState _instance;

    public static GameState Instance
    {
        get { return _instance; }
    }

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

    public List<FoundElement> FoundElements ;
    
    void Start()
    {
        FoundElements = new List<FoundElement>();
    }

    public void ElementIsInstantiated(Element element)
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

        var hasElement = false;
        foreach (var savedFoundElement in FoundElements)
        {
            if (savedFoundElement.ElementName == foundElement.ElementName && savedFoundElement.Level == foundElement.Level)
            {
                hasElement = true;
            }
        }

        if (!hasElement)
        {
            FoundElements.Add(foundElement);
        }
    }
}