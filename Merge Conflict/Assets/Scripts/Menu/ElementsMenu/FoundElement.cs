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