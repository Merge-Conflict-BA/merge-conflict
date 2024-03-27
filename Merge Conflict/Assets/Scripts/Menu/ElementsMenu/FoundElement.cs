﻿public class FoundElement
{
    public string ElementName { get; }
    public int Level { get; }

    public int CountPurchased { get; set; }

    public FoundElement(string elementName, int level, int countPurchased = 0)
    {
        ElementName = elementName;
        Level = level;
        CountPurchased = countPurchased;
    }

    public string ToCardTitle()
    {
        return "Card" + ElementName + "Lvl" + Level;
    }
}