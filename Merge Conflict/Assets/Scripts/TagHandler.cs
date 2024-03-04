/**********************************************************************************************************************
Name:          TagHandler
Description:   
Author(s):     Markus Haubold
Date:          2024-03-04
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;
public class TagHandler
{
    private readonly string _componentTag = "component";
    private readonly string _trashcanTag = "trashcan";
    
     public bool IsComponent(GameObject gameObject)
    {
        return gameObject.CompareTag(_componentTag);
    }

    public bool IsTrashcan(GameObject gameObject)
    {
        return gameObject.CompareTag(_trashcanTag);
    }
}
