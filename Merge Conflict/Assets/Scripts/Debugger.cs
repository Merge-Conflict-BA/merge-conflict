/**********************************************************************************************************************
Name:          Dragging
Description:   Custom debugging class to log message automaticly only during the development in the unity editor
Author(s):     Markus Haubold
Date:          2024-02-20
Version:       V1.0 
TODO:          - setup logMessage(), logWarning() and logError()
**********************************************************************************************************************/

using UnityEngine;

public class Debugger
{
    public void logMessage(string message)
    {
        #if UNITY_EDITOR
            Debug.Log(message);   
        #endif
    }

    public void logWarning(string message)
    {
        #if UNITY_EDITOR
            Debug.LogWarning(message);   
        #endif
    }

    public void logError(string message)
    {
        #if UNITY_EDITOR
            Debug.LogError(message);   
        #endif
    }
}