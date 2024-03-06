/**********************************************************************************************************************
Name:          Tag
Description:   Contains all tags, which are used to identify objects. With CompareWithGameObjectTag() its possible to compare the tag 
               from the given GameObject with a tag from the Tags "list".
Author(s):     Markus Haubold
Date:          2024-03-06
Version:       V1.2
TODO:          - ongoing: add new tags
**********************************************************************************************************************/

using UnityEngine;

//add all tags which are used in unity!
public enum Tags
{
    Component,
    Trashcan,
    ConveyorBelt,
    ConveyorBeltDiagonal
}

public static class TagComparator
{
    public static bool CompareWithGameObjectTag(this Tags tag, GameObject gameObject)
    {
        return tag switch
        {
            Tags.Component => gameObject.CompareTag(tag.ToString()),
            Tags.Trashcan => gameObject.CompareTag(tag.ToString()),
            Tags.ConveyorBelt => gameObject.CompareTag(tag.ToString()),
            Tags.ConveyorBeltDiagonal => gameObject.CompareTag(tag.ToString()),
            _ => false,
        };
    }
}
