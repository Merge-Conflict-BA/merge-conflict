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
