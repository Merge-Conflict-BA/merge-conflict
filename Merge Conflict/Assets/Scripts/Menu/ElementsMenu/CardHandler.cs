/**********************************************************************************************************************
Name:          CardHandler
Description:   Updates the cards with sprite and price for each found element

Author(s):     Simeon Baumann
Date:          2024-03-23
Version:       V1.0
TODO:          - check if the player has enough money
**********************************************************************************************************************/

using System;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

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
    private float _startPrice;
    private float _increaseFactor;
    
    private float _clickStartTime;
    private const float MaxTimeOfClick = 0.3f;

    public void UpdateSprite(FoundElement element)
    {
        _cardElement = element;
        
        Image image = GetComponent<Image>();
        int index = element.Level - 1;

        string elementName = element.ElementName;

        switch (elementName)
        {
            case var _ when elementName.Equals(CaseComponent.Name):
                image.sprite = CaseSprites[index];
                _startPrice = Components.caseStartPrice[index];
                _increaseFactor = Components.caseIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(CPUComponent.Name):
                image.sprite = CPUSprites[index];
                _startPrice = Components.cpuStartPrice[index];
                _increaseFactor = Components.cpuIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(GPUComponent.Name):
                image.sprite = GPUSprites[index];
                _startPrice = Components.gpuStartPrice[index];
                _increaseFactor = Components.gpuIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(HDDComponent.Name):
                image.sprite = HDDSprites[index];
                _startPrice = Components.hddStartPrice[index];
                _increaseFactor = Components.hddIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(MBComponent.Name):
                image.sprite = MotherboardSprites[index];
                _startPrice = Components.mbStartPrice[index];
                _increaseFactor = Components.mbIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(PowersupplyComponent.Name):
                image.sprite = PowerSupplySprites[index];
                _startPrice = Components.powersupplyStartPrice[index];
                _increaseFactor = Components.powersupplyIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(RAMComponent.Name):
                image.sprite = RAMSprites[index];
                _startPrice = Components.ramStartPrice[index];
                _increaseFactor = Components.ramIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(Trash.Name):
            default:
                image.sprite = DefaultSprite;
                _startPrice = 0;
                _increaseFactor = 0;
                break;
        }

        UpdatePrice();
    }

    public void OnMouseUpAsButton()
    {
        // if the user clicks longer than _maxTimeOfClick on a card it is handled as a scroll - not as a click
        if ((Time.time - _clickStartTime) > MaxTimeOfClick)
        {
            return;
        }
        
        // todo: check if the player has enough money
        // buy this particular Element
        BuyElement();
    }

    private void OnMouseDown()
    {
        _clickStartTime = Time.time;
    }

    private void BuyElement()
    {
        string elementName = _cardElement.ElementName;
        Element element = elementName switch
        {
            _ when elementName.Equals(CaseComponent.Name) => Components.CreateCase(),
            _ when elementName.Equals(CPUComponent.Name) => Components.CPU,
            _ when elementName.Equals(GPUComponent.Name) => Components.GPU,
            _ when elementName.Equals(HDDComponent.Name) => Components.HDD,
            _ when elementName.Equals(MBComponent.Name) => Components.CreateMB(),
            _ when elementName.Equals(PowersupplyComponent.Name) => Components.Powersupply,
            _ when elementName.Equals(RAMComponent.Name) => Components.RAM,
            _ => null
        };

        if (element == null)
        {
            Debugger.LogError("Could not get Component from Card element.");
            return;
        }

        element.tier = _cardElement.Level;
        
        element.InstantiateGameObjectAndAddTexture(ComponentSpawner.Instance.GetRandomPositionOnDesk());
        
        // Update count of purchased elements and the price of the next card
        _cardElement.CountPurchased++;
        FoundElementsHandler.Instance.UpdateStoredElement(_cardElement);
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        float currentPrice = (float)Math.Floor(_startPrice * Math.Pow(_increaseFactor, _cardElement.CountPurchased));
        
        var priceText = GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = $"$ {currentPrice}";
    }
}
