using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        
    }
}
