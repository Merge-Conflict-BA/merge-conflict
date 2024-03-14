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

    public static float scale = 15f;
    public static float scaleSubComponents = 1f;
    public static float[] subComponentTextureValues = { 0f, 0f, scaleSubComponents }; //  float[] = {sizeWidth, sizeHeight, scale}

    public ElementTexture[] caseTexture;
    public static float[] caseTextureValues = { 5f, 5f, scale };

    public ElementTexture[] powersupplyTexture;
    public static float[] powersupplyTextureValues = { 5.5f, 4.5f, scale };

    public ElementTexture[] hddTexture;
    public static float[] hddTextureValues = { 2.7f, 4.5f, scale };

    public ElementTexture[] mbTexture;
    public static float[] mbTextureValues = { 4.5f, 6f, scale };

    public ElementTexture[] cpuTexture;
    public static float[] cpuTextureValues = { 3f, 3.5f, scale };

    public ElementTexture[] ramTexture;
    public static float[] ramTextureValues = { 3.5f, 2.5f, scale };

    public ElementTexture[] gpuTexture;
    public static float[] gpuTextureValues = { 4.5f, 3f, scale };

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
    public static float[][] trashTextureValues = {
            new float[] { 4f, 4f, scale },
            new float[] { 3f, 3.5f, scale },
            new float[] { 3f, 4f, scale }
        }; // banana, bug, can

    public ElementTexture defaultTexture;
    public static float[] defaultTextureValues = { 5f, 5f, scale };


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
        ElementTexture texture = defaultTexture;
        float[] textureValues = defaultTextureValues;

        switch (element)
        {
            case CaseComponent:
                texture = caseTexture[index];
                textureValues = caseTextureValues;
                break;

            case PowersupplyComponent:
                texture = powersupplyTexture[index];
                textureValues = powersupplyTextureValues;
                break;

            case HDDComponent:
                texture = hddTexture[index];
                textureValues = hddTextureValues;
                break;

            case MBComponent:
                texture = mbTexture[index];
                textureValues = mbTextureValues;
                break;

            case CPUComponent:
                texture = cpuTexture[index];
                textureValues = cpuTextureValues;
                break;

            case RAMComponent:
                texture = ramTexture[index];
                textureValues = ramTextureValues;
                break;

            case GPUComponent:
                texture = gpuTexture[index];
                textureValues = gpuTextureValues;
                break;

            case Trash trash:
                texture = trashTexture[(int)trash.trashVariant];
                textureValues = trashTextureValues[(int)trash.trashVariant];
                break;

            default:
                Debug.LogWarning("There is no matching texture for: " + element);
                texture = defaultTexture;
                break;
        }

        // Apply texture values for Component
        texture.sizeWidth = textureValues[0];
        texture.sizeHeight = textureValues[1];
        texture.sizeScaleX = textureValues[2];
        texture.sizeScaleY = textureValues[2];

        return texture;
    }

    public List<ElementTexture> GetSlotComponentTextures(Element element)
    {

        List<ElementTexture> listOfSlotComponentTextures = new();
        int levelToIndex = 1;

        if (element is MBComponent mb)
        {
            if (mb.cpu != null) { listOfSlotComponentTextures.Add(mbCPUSlotTexture[mb.cpu.level - levelToIndex]); }
            if (mb.gpu != null) { listOfSlotComponentTextures.Add(mbGPUSlotTexture[mb.gpu.level - levelToIndex]); }
            if (mb.ram != null) { listOfSlotComponentTextures.Add(mbRAMSlotTexture[mb.ram.level - levelToIndex]); }
        }

        if (element is CaseComponent cs)
        {
            if (cs.powersupply != null)     { listOfSlotComponentTextures.Add(casePowersupplySlotTexture[cs.powersupply.level - levelToIndex]); }
            if (cs.hdd != null)             { listOfSlotComponentTextures.Add(caseHDDSlotTexture[cs.hdd.level - levelToIndex]); }

            if (cs.motherboard != null)
            {
                listOfSlotComponentTextures.Add(caseMBSlotTexture[cs.motherboard.level - levelToIndex]);

                if (cs.motherboard.cpu != null) { listOfSlotComponentTextures.Add(caseCPUSlotTexture[cs.motherboard.cpu.level - levelToIndex]); }
                if (cs.motherboard.ram != null) { listOfSlotComponentTextures.Add(caseRAMSlotTexture[cs.motherboard.ram.level - levelToIndex]); }
                if (cs.motherboard.gpu != null) { listOfSlotComponentTextures.Add(caseGPUSlotTexture[cs.motherboard.gpu.level - levelToIndex]); }
            } 
        }

        // Apply texture values for sub components
        foreach (ElementTexture texture in listOfSlotComponentTextures)
        {
            texture.sizeWidth = subComponentTextureValues[0];
            texture.sizeHeight = subComponentTextureValues[1];
            texture.sizeScaleX = subComponentTextureValues[2];
            texture.sizeScaleY = subComponentTextureValues[2];
        }

        return listOfSlotComponentTextures;
    }

}