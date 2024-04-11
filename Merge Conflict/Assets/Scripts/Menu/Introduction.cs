using TMPro;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    private static Introduction _instance;
    public static Introduction Instance { get { return _instance; } }
    public ScreenSwipe ScreenSwipe;
    private int FirstScreen = 0;


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

    public void ResetToFirstScreen()
    {
        ScreenSwipe.GoToScreen(FirstScreen);
    }
}