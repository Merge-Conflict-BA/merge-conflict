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

        int index = element.level - 1;

        switch (element)
        {
            case CaseComponent:
                return caseTexture[index];

            case PowersupplyComponent:
                return powersupplyTexture[index];

            case HDDComponent:
                return hddTexture[index];

            case MBComponent:
                return mbTexture[index];

            case CPUComponent:
                return cpuTexture[index];

            case RAMComponent:
                return ramTexture[index];

            case GPUComponent:
                return gpuTexture[index];

            case Trash trash:
                return trashTexture[(int)trash.trashVariant];

            default:
                Debug.LogWarning("There is no matching texture for: " + element);
                return defaultTexture;
        }
    }

    public List<ElementTexture> GetSlotComponentTextures(Element element)
    {

        List<ElementTexture> listOfSlotComponentTextures = new();
        int levelIndexIndicator = 1;

        if (element is MBComponent mb)
        {
            if (mb.cpu != null) { listOfSlotComponentTextures.Add(mbCPUSlotTexture[mb.cpu.level - levelIndexIndicator]); }
            if (mb.gpu != null) { listOfSlotComponentTextures.Add(mbGPUSlotTexture[mb.gpu.level - levelIndexIndicator]); }
            if (mb.ram != null) { listOfSlotComponentTextures.Add(mbRAMSlotTexture[mb.ram.level - levelIndexIndicator]); }
        }

        if (element is CaseComponent cs)
        {
            if (cs.powersupply != null) { listOfSlotComponentTextures.Add(casePowersupplySlotTexture[cs.powersupply.level - levelIndexIndicator]); }
            if (cs.hdd != null) { listOfSlotComponentTextures.Add(caseHDDSlotTexture[cs.hdd.level - levelIndexIndicator]); }
            if (cs.motherboard != null) { listOfSlotComponentTextures.Add(caseMBSlotTexture[cs.motherboard.level - levelIndexIndicator]); }
            if (cs.motherboard.cpu != null) { listOfSlotComponentTextures.Add(caseCPUSlotTexture[cs.motherboard.cpu.level - levelIndexIndicator]); }
            if (cs.motherboard.ram != null) { listOfSlotComponentTextures.Add(caseRAMSlotTexture[cs.motherboard.ram.level - levelIndexIndicator]); }
            if (cs.motherboard.gpu != null) { listOfSlotComponentTextures.Add(caseGPUSlotTexture[cs.motherboard.gpu.level - levelIndexIndicator]); }
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