public class FoundElement
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

    public bool IsEqual(FoundElement foundElement)
    {
        return ElementName == foundElement.ElementName && Level == foundElement.Level;
    }

    public string GetPlayerPrefsKey()
    {
        return $"{ElementName}_{Level}";
    }
}