/**********************************************************************************************************************
Name:          GamePauseManager
Description:   Handle the conveyorbelt and the spawning according to the state of the game (paused or not)
Author(s):     Markus Haubold
Date:          2024-04-11
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    private static GamePauseManager _instance;
    public static GamePauseManager Instance { get { return _instance; } }
    public GameObject ConveyorbeltVertical;
    public GameObject ConveyorbeltHorizontal;
    const bool Stopped = false;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    //pause the game by stopping the conveyor belt and the spawning of new objects
    public void Pause()
    {
        StartStopConveyorbelt(Stopped);
        ComponentSpawner.Instance.PauseSpawn();
    }

    //continue the game by start the conveyor belt and the spawning of new objects
    public void Continue()
    {
        StartStopConveyorbelt(!Stopped);
        ComponentSpawner.Instance.ResumeSpawn();
    }

    private void StartStopConveyorbelt(bool state)
    {
        ConveyorbeltVertical.SetActive(state);
        ConveyorbeltHorizontal.SetActive(state);
    }
}