/**********************************************************************************************************************
Name:          CardHandler
Description:   Updates the cards with sprite and price for each found element

Author(s):     Simeon Baumann
Date:          2024-03-23
Version:       V1.0
TODO:          - price to buy
**********************************************************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    [Header("Sprites for each card")]
    public Sprite[] CaseSprites;
    public Sprite[] CPUSprites;
    public Sprite[] GPUSprites;
    public Sprite[] HDDSprites;
    public Sprite[] MotherboardSprites;
    public Sprite[] PowerSupplySprites;
    public Sprite[] RAMSprites;
    public Sprite DefaultSprite;

    // Defines which Element this Object is
    private FoundElement _cardElement;

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

        _cardElement = element;
    }

    public void OnMouseUpAsButton()
    {
        // buy this particular Element
        BuyElement();
    }

    private void BuyElement()
    {
        // todo: get random Position on Desk
        Vector2 positionOnDesk = new Vector2(Screen.width / 2, Screen.height / 2);

        Element element = _cardElement.ElementName switch
        {
            ElementName.Case => Components.CreateCase(),
            ElementName.CPU => Components.CPU,
            ElementName.GPU => Components.GPU,
            ElementName.HDD => Components.HDD,
            ElementName.Motherboard => Components.CreateMB(),
            ElementName.PowerSupply => Components.Powersupply,
            ElementName.RAM => Components.RAM,
            _ => null
        };

        if (element == null)
        {
            Debugger.LogError("Could not get Component from Card element.");
            return;
        }

        element.level = _cardElement.Level;

        element.InstantiateGameObjectAndAddTexture(positionOnDesk);
    }
}
