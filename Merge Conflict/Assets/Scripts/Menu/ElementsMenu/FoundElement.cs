public class FoundElement
{
    public string ElementName { get; }
    public int Tier { get; }

    public int CountPurchased { get; set; }

    public FoundElement(string elementName, int tier, int countPurchased = 0)
    {
        ElementName = elementName;
        Tier = tier;
        CountPurchased = countPurchased;
    }

    public string ToCardTitle()
    {
        return "Card" + ElementName + "Tier" + Tier;
    }

    public bool IsEqual(FoundElement foundElement)
    {
        return ElementName == foundElement.ElementName && Tier == foundElement.Tier;
    }

    public string GetPlayerPrefsKey()
    {
        return $"{ElementName}_{Tier}";
    }
}