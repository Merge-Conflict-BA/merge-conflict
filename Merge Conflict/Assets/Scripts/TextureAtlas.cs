/**********************************************************************************************************************
Name:          TextureAtlas
Description:   Elements data structure for texture.  

Author(s):     Daniel Rittrich
Date:          2024-02-27
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAtlas : MonoBehaviour
{

    private static TextureAtlas _instance;
    public static TextureAtlas Instance { get { return _instance; } }

    public ElementTexture caseTexture;
    public ElementTexture powersupplyTexture;
    public ElementTexture hddTexture;
    public ElementTexture mbTexture;
    public ElementTexture cpuTexture;
    public ElementTexture ramTexture;
    public ElementTexture gpuTexture;
    public ElementTexture trashTexture;
    public ElementTexture defaultTexture;


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


    public ElementTexture getTexture(Element element)
    {
        switch (element)
        {
            case CaseComponent:
                return caseTexture;

            case PowersupplyComponent:
                return powersupplyTexture;

            case HDDComponent:
                return hddTexture;

            case MBComponent:
                return mbTexture;

            case CPUComponent:
                return cpuTexture;

            case RAMComponent:
                return ramTexture;

            case GPUComponent:
                return gpuTexture;

            case Trash:
                return trashTexture;

            default:
                Debug.LogWarning("There is no matching texture for: " + element);
                return defaultTexture;
        }
    }



    void Start()
    {

    }

    void Update()
    {

    }
}