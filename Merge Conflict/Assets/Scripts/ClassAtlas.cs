/**********************************************************************************************************************
Name:          ClassAtlas
Description:   For getting the correct class.  

Author(s):     Daniel Rittrich
Date:          2024-02-28
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassAtlas : MonoBehaviour
{

    private static ClassAtlas _instance;
    public static ClassAtlas Instance { get { return _instance; } }


    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }


    public Element? GetClass(GameObject element)
    {

        if (element.GetComponent<CaseComponent>() != null)
        {
            return new CaseComponent();
        }
        else if (element.GetComponent<PowersupplyComponent>() != null)
        {
            return new PowersupplyComponent();
        }
        else if (element.GetComponent<HDDComponent>() != null)
        {
            return new HDDComponent();
        }
        else if (element.GetComponent<MBComponent>() != null)
        {
            return new MBComponent();
        }
        else if (element.GetComponent<CPUComponent>() != null)
        {
            return new CPUComponent();
        }
        else if (element.GetComponent<RAMComponent>() != null)
        {
            return new RAMComponent();
        }
        else if (element.GetComponent<GPUComponent>() != null)
        {
            return new GPUComponent();
        }
        else if (element.GetComponent<Trash>() != null)
        {
            return new Trash();
        }
        else
        {
            Debugger.LogError("No matching class found.");
            return null;
        }
    }


    void Start()
    {

    }

    void Update()
    {

    }
}