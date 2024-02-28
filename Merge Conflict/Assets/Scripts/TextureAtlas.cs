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

    public ElementTexture caseTextureLvl1;
    public ElementTexture caseTextureLvl2;
    public ElementTexture caseTextureLvl3;
    public ElementTexture caseTextureLvl4;
    public ElementTexture powersupplyTextureLvl1;
    public ElementTexture powersupplyTextureLvl2;
    public ElementTexture powersupplyTextureLvl3;
    public ElementTexture powersupplyTextureLvl4;
    public ElementTexture hddTextureLvl1;
    public ElementTexture hddTextureLvl2;
    public ElementTexture hddTextureLvl3;
    public ElementTexture hddTextureLvl4;
    public ElementTexture mbTextureLvl1;
    public ElementTexture mbTextureLvl2;
    public ElementTexture mbTextureLvl3;
    public ElementTexture mbTextureLvl4;
    public ElementTexture cpuTextureLvl1;
    public ElementTexture cpuTextureLvl2;
    public ElementTexture cpuTextureLvl3;
    public ElementTexture cpuTextureLvl4;
    public ElementTexture ramTextureLvl1;
    public ElementTexture ramTextureLvl2;
    public ElementTexture ramTextureLvl3;
    public ElementTexture ramTextureLvl4;
    public ElementTexture gpuTextureLvl1;
    public ElementTexture gpuTextureLvl2;
    public ElementTexture gpuTextureLvl3;
    public ElementTexture gpuTextureLvl4;
    public ElementTexture cpuEquipedTextureLvl1;
    public ElementTexture cpuEquipedTextureLvl2;
    public ElementTexture cpuEquipedTextureLvl3;
    public ElementTexture cpuEquipedTextureLvl4;
    public ElementTexture ramEquipedTextureLvl1;
    public ElementTexture ramEquipedTextureLvl2;
    public ElementTexture ramEquipedTextureLvl3;
    public ElementTexture ramEquipedTextureLvl4;
    public ElementTexture gpuEquipedTextureLvl1;
    public ElementTexture gpuEquipedTextureLvl2;
    public ElementTexture gpuEquipedTextureLvl3;
    public ElementTexture gpuEquipedTextureLvl4;
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
                if (element.itemLevel == 1) { return caseTextureLvl1; }
                if (element.itemLevel == 2) { return caseTextureLvl2; }
                if (element.itemLevel == 3) { return caseTextureLvl3; }
                if (element.itemLevel == 4) { return caseTextureLvl4; }
                return defaultTexture;

            case PowersupplyComponent:
                if (element.itemLevel == 1) { return powersupplyTextureLvl1; }
                if (element.itemLevel == 2) { return powersupplyTextureLvl2; }
                if (element.itemLevel == 3) { return powersupplyTextureLvl3; }
                if (element.itemLevel == 4) { return powersupplyTextureLvl4; }
                return defaultTexture;

            case HDDComponent:
                if (element.itemLevel == 1) { return hddTextureLvl1; }
                if (element.itemLevel == 2) { return hddTextureLvl2; }
                if (element.itemLevel == 3) { return hddTextureLvl3; }
                if (element.itemLevel == 4) { return hddTextureLvl4; }
                return defaultTexture;

            case MBComponent:
                if (element.itemLevel == 1) { return mbTextureLvl1; }
                if (element.itemLevel == 2) { return mbTextureLvl2; }
                if (element.itemLevel == 3) { return mbTextureLvl3; }
                if (element.itemLevel == 4) { return mbTextureLvl4; }
                return defaultTexture;

            case CPUComponent:
                if (element.itemLevel == 1) { return cpuTextureLvl1; }
                if (element.itemLevel == 2) { return cpuTextureLvl2; }
                if (element.itemLevel == 3) { return cpuTextureLvl3; }
                if (element.itemLevel == 4) { return cpuTextureLvl4; }
                return defaultTexture;

            case RAMComponent:
                if (element.itemLevel == 1) { return ramTextureLvl1; }
                if (element.itemLevel == 2) { return ramTextureLvl2; }
                if (element.itemLevel == 3) { return ramTextureLvl3; }
                if (element.itemLevel == 4) { return ramTextureLvl4; }
                return defaultTexture;

            case GPUComponent:
                if (element.itemLevel == 1) { return gpuTextureLvl1; }
                if (element.itemLevel == 2) { return gpuTextureLvl2; }
                if (element.itemLevel == 3) { return gpuTextureLvl3; }
                if (element.itemLevel == 4) { return gpuTextureLvl4; }
                return defaultTexture;

            case Trash:
                return trashTexture;

            default:
                Debug.LogWarning("There is no matching texture for: " + element);
                return defaultTexture;
        }
    }

    public ElementTexture getEquipedTexture(int type, int equipedItemLvl)
    {
        switch (type)
        {
            case 1:
                if (equipedItemLvl == 1) { return cpuEquipedTextureLvl1; }
                if (equipedItemLvl == 2) { return cpuEquipedTextureLvl2; }
                if (equipedItemLvl == 3) { return cpuEquipedTextureLvl3; }
                if (equipedItemLvl == 4) { return cpuEquipedTextureLvl4; }
                return defaultTexture;

            case 2:
                if (equipedItemLvl == 1) { return ramEquipedTextureLvl1; }
                if (equipedItemLvl == 2) { return ramEquipedTextureLvl2; }
                if (equipedItemLvl == 3) { return ramEquipedTextureLvl3; }
                if (equipedItemLvl == 4) { return ramEquipedTextureLvl4; }
                return defaultTexture;

            case 3:
                if (equipedItemLvl == 1) { return gpuEquipedTextureLvl1; }
                if (equipedItemLvl == 2) { return gpuEquipedTextureLvl2; }
                if (equipedItemLvl == 3) { return gpuEquipedTextureLvl3; }
                if (equipedItemLvl == 4) { return gpuEquipedTextureLvl4; }
                return defaultTexture;

            default:
                Debug.LogWarning("There is no matching equipedTexture for: " + type);
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