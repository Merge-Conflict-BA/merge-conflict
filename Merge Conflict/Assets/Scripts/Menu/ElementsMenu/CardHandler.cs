using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    public Sprite[] CaseSprites;
    public Sprite[] CPUSprites;
    public Sprite[] GPUSprites;
    public Sprite[] HDDSprites;
    public Sprite[] MotherboardSprites;
    public Sprite[] PowerSupplySprites;
    public Sprite[] RAMSprites;
    public Sprite DefaultSprite;

    public void UpdateSprite(FoundElement element)
    {
        Image image = GetComponent<Image>();

        image.sprite = element.ElementName switch
        {
            ElementName.Case => CaseSprites[element.Level - 1],
            ElementName.CPU => CPUSprites[element.Level - 1],
            ElementName.GPU => GPUSprites[element.Level - 1],
            ElementName.HDD => HDDSprites[element.Level - 1],
            ElementName.Motherboard => MotherboardSprites[element.Level - 1],
            ElementName.PowerSupply => PowerSupplySprites[element.Level - 1],
            ElementName.RAM => RAMSprites[element.Level - 1],
            _ => DefaultSprite
        };
    }
}
