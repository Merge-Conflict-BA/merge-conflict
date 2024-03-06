/**********************************************************************************************************************
Name:          TextureAtlas
Description:   Elements data structure for texture.  

Author(s):     Daniel Rittrich
Date:          2024-02-27
Version:       V2.1 
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
    public ElementTexture[] mbCPUSlotTexture;
    public ElementTexture[] mbRAMSlotTexture;
    public ElementTexture[] mbGPUSlotTexture;
    public ElementTexture[] casePowersupplySlotTexture;
    public ElementTexture[] caseHDDSlotTexture;
    public ElementTexture[] caseMBSlotTexture;
    public ElementTexture[] caseCPUSlotTexture;
    public ElementTexture[] caseRAMSlotTexture;
    public ElementTexture[] caseGPUSlotTexture;
    public ElementTexture[] trashTexture;
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


    public ElementTexture GetComponentTexture(Element element)
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
                return trashTexture[element.GetComponent<Trash>().trashVariant];

            default:
                Debug.LogWarning("There is no matching texture for: " + element);
                return defaultTexture;
        }
    }

    public List<ElementTexture> GetSlotComponentTextures(Element element)
    {

        List<ElementTexture> listOfSlotComponentTextures = new();

        if (element is MBComponent mb)
        {
            if (mb.cpu != null) { listOfSlotComponentTextures.Add(mbCPUSlotTexture[mb.cpu.level - 1]); }
            if (mb.gpu != null) { listOfSlotComponentTextures.Add(mbGPUSlotTexture[mb.gpu.level - 1]); }
            if (mb.ram != null) { listOfSlotComponentTextures.Add(mbRAMSlotTexture[mb.ram.level - 1]); }
        }

        if (element is CaseComponent cs)
        {
            if (cs.powersupply != null) { listOfSlotComponentTextures.Add(casePowersupplySlotTexture[cs.powersupply.level - 1]); }
            if (cs.hdd != null) { listOfSlotComponentTextures.Add(caseHDDSlotTexture[cs.hdd.level - 1]); }
            if (cs.motherboard != null) { listOfSlotComponentTextures.Add(caseMBSlotTexture[cs.motherboard.level - 1]); }
            if (cs.motherboard.cpu != null) { listOfSlotComponentTextures.Add(caseCPUSlotTexture[cs.motherboard.cpu.level - 1]); }
            if (cs.motherboard.ram != null) { listOfSlotComponentTextures.Add(caseRAMSlotTexture[cs.motherboard.ram.level - 1]); }
            if (cs.motherboard.gpu != null) { listOfSlotComponentTextures.Add(caseGPUSlotTexture[cs.motherboard.gpu.level - 1]); }
        }

        if (listOfSlotComponentTextures.Count == 0)
        {
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