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

    public void Pause()
    {
        StartStopConveyorbelt(Stopped);
        ComponentSpawner.Instance.PauseSpawn();
    }

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