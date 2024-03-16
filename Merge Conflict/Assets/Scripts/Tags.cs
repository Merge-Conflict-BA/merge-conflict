/**********************************************************************************************************************
Name:          Tag
Description:   Contains all tags, which are used to identify objects. With UsedByGameObject() its possible to compare the tag 
               from the given GameObject with a tag from the Tags enum.
Author(s):     Markus Haubold
Date:          2024-03-08
Version:       V1.3
TODO:          - ongoing: add new tags
**********************************************************************************************************************/

using UnityEngine;

//add all tags which are used in unity!
public enum Tags
{
    Untagged,
    Component,
    SubComponent,
    Trashcan,
    ConveyorBelt,
    ConveyorBeltDiagonal
}

public static class TagComparator
{
    public static bool UsedByGameObject(this Tags tag, GameObject gameObject)
    {
        return gameObject.CompareTag(tag.ToString());
    }
}