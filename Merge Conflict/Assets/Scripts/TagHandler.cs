/**********************************************************************************************************************
Name:          TagHandler
Description:   Contains all tags, which are used to identify objects. With CompareTags() its possible to compare the tag 
               from the given GameObject with a tag from the PossibleTags "list".
Author(s):     Markus Haubold
Date:          2024-03-04
Version:       V1.0
TODO:          - ongoing: add new tags
**********************************************************************************************************************/

using UnityEngine;

//add all tags which are used in unity!
public static class TagHandler
{
    public enum PossibleTags
    {
        component,
        trashcan,
        ConveyorBelt,
        ConveyorBeltDiagonal
    }

    public static bool CompareTags(GameObject gameObject, PossibleTags possibleTag)
    {
        if (gameObject.CompareTag(possibleTag.ToString())) { return true; }

        return false;
    }
}