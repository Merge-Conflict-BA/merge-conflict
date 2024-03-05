/**********************************************************************************************************************
Name:          Tag
Description:   Contains all tags, which are used to identify objects. With CompareTags() its possible to compare the tag 
               from the given GameObject with a tag from the PossibleTags "list".
Author(s):     Markus Haubold
Date:          2024-03-04
Version:       V1.0
TODO:          - ongoing: add new tags
**********************************************************************************************************************/

using UnityEngine;

//add all tags which are used in unity!
public static class Tags
{
    public enum Tag
    {
        Component,
        Trashcan,
        ConveyorBelt,
        ConveyorBeltDiagonal
    }

    public static bool CompareWithGameObjectTag(this Tag tags, GameObject gameObject)
    {
        return tags switch
        {
            Tag.Component => gameObject.CompareTag(tags.ToString()),
            Tag.Trashcan => gameObject.CompareTag(tags.ToString()),
            Tag.ConveyorBelt => gameObject.CompareTag(tags.ToString()),
            Tag.ConveyorBeltDiagonal => gameObject.CompareTag(tags.ToString()),
            _ => false,
        };
    }
}
