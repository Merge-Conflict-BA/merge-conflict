public class FoundElement
{
    public ElementName ElementName { get; }
    public int Level { get; }

    public int CountPurchased { get; set; }

    public FoundElement(ElementName elementName, int level, int countPurchased = 0)
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