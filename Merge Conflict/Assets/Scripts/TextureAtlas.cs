/**********************************************************************************************************************
Name:          TextureAtlas
Description:   Elements data structure for texture.  

Author(s):     Daniel Rittrich
Date:          2024-02-27
Version:       V2.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAtlas : MonoBehaviour
{

    private static TextureAtlas _instance;
    public static TextureAtlas Instance { get { return _instance; } }

    public ElementTexture[] caseTexture;
    public ElementTexture[] powersupplyTexture;
    public ElementTexture[] hddTexture;
    public ElementTexture[] mbTexture;
    public ElementTexture[] cpuTexture;
    public ElementTexture[] ramTexture;
    public ElementTexture[] gpuTexture;
    public ElementTexture[] cpuSlotComponentTexture;
    public ElementTexture[] ramSlotComponentTexture;
    public ElementTexture[] gpuSlotComponentTexture;
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


    public ElementTexture getComponentTexture(Element element)
    {
        switch (element)
        {
            case CaseComponent:
                return caseTexture[element.level - 1];

            case PowersupplyComponent:
                return powersupplyTexture[element.level - 1];

            case HDDComponent:
                return hddTexture[element.level - 1];

            case MBComponent:
                return mbTexture[element.level - 1];

            case CPUComponent:
                return cpuTexture[element.level - 1];

            case RAMComponent:
                return ramTexture[element.level - 1];

            case GPUComponent:
                return gpuTexture[element.level - 1];

            case Trash:
                return trashTexture;

            default:
                Debug.LogWarning("There is no matching texture for: " + element);
                return defaultTexture;
        }
    }

    public List<ElementTexture> getSlotComponentTexture(Element element)
    {
        if (!(element is MBComponent mb)) { return null; }

        List<ElementTexture> listOfSlotComponentTextures = new List<ElementTexture>();

        if (mb.cpu != null)
        {
            listOfSlotComponentTextures.Add(cpuSlotComponentTexture[mb.cpu.level]);
        }

        if (mb.gpu != null)
        {
            listOfSlotComponentTextures.Add(gpuSlotComponentTexture[mb.gpu.level]);
        }

        if (mb.ram != null)
        {
            listOfSlotComponentTextures.Add(ramSlotComponentTexture[mb.ram.level]);
        }

        if (listOfSlotComponentTextures.Count == 0)
        {
            Debug.LogWarning("There is no matching slotComponentTexture for: " + element);
            return null;
        }

        return listOfSlotComponentTextures;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}