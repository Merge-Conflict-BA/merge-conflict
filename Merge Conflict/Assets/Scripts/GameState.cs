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

    public FoundElement[] FoundElements;

    // Start is called before the first frame update
    void Start()
    {
        FoundElements = new[]
        {
            new FoundElement(ElementName.Case, 1), 
            new FoundElement(ElementName.CPU, 1),
            new FoundElement(ElementName.Motherboard, 1),
            new FoundElement(ElementName.Motherboard, 2)
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
}

public class FoundElement
{
    public ElementName ElementName { get; }
    public int Level { get; }

    public FoundElement(ElementName elementName, int level)
    {
        ElementName = elementName;
        Level = level;
    }

    public string ToCardTitle()
    {
        return "Card" + ElementName + "Lvl" + Level;
    }
}

public enum ElementName
{
    Case,
    CPU,
    GPU,
    HDD,
    Motherboard,
    PowerSupply,
    RAM
}