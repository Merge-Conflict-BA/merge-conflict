using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    private static GamePauseManager _instance;
    public static GamePauseManager Instance { get { return _instance; } }
    public GameObject ConveyorbeltVertical;
    public GameObject ConveyorbeltHorizontal;
    //public GameObject ComponentSpawner;
    private bool gameState;
    const bool GamePaused = false;


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
        SetGameObjectActivePropertie(GamePaused);
        ComponentSpawner.Instance.PauseSpawn();
    }

    public void Continue()
    {
        SetGameObjectActivePropertie(!GamePaused);
        ComponentSpawner.Instance.ResumeSpawn();
    }

    private void SetGameObjectActivePropertie(bool propertyState)
    {
        ConveyorbeltVertical.SetActive(propertyState);
        ConveyorbeltHorizontal.SetActive(propertyState);
        //ComponentSpawner.SetActive(propertyState);
    }
}