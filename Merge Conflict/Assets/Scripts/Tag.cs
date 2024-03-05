/**********************************************************************************************************************
Name:          TagHandler
Description:   
Author(s):     Markus Haubold
Date:          2024-03-04
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;
public static class Tag
{    
     public static bool IsComponent(GameObject gameObject)
    {
        return gameObject.CompareTag("component");
    }

    public static bool IsTrashcan(GameObject gameObject)
    {
        return gameObject.CompareTag("trashcan");
    }
}
