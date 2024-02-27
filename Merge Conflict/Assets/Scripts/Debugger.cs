/**********************************************************************************************************************
Name:          Dragging
Description:   Custom debugging class to log message automaticly only during the development in the unity editor
Author(s):     Markus Haubold
Date:          2024-02-20
Version:       V1.1 
**********************************************************************************************************************/

using UnityEngine;

public static class Debugger
{
    public static void LogMessage(string message)
    {
        #if UNITY_EDITOR
            Debug.Log(message);   
        #endif
    }

    public static void LogWarning(string message)
    {
        #if UNITY_EDITOR
            Debug.LogWarning(message);   
        #endif
    }

    public static void LogError(string message)
    {
        #if UNITY_EDITOR
            Debug.LogError(message);   
        #endif
    }
}