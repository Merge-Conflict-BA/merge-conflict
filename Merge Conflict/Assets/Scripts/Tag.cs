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
public static class Tag
{
    public enum Tags
    {
        component,
        trashcan,
        ConveyorBelt,
        ConveyorBeltDiagonal
    }

    public static bool CompareTags(this GameObject gameObject, Tags tags)
    {
        return tags switch
        {
            Tags.component => gameObject.CompareTag(tags.ToString()),
            Tags.trashcan => gameObject.CompareTag(tags.ToString()),
            Tags.ConveyorBelt => gameObject.CompareTag(tags.ToString()),
            Tags.ConveyorBeltDiagonal => gameObject.CompareTag(tags.ToString()),
            _ => false,
        };
    }
}