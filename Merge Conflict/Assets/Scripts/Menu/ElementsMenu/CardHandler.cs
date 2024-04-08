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
using UnityEngine.UI;
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
    
    [Header("Button to purchase Element")]
    public Button purchaseButton;

    [Header("Text to show after a purchase is done")]
    public GameObject purchasedTextObject;

    // Defines which Element this Object is
    private FoundElement _cardElement;
    private float _startPrice;
    private float _increaseFactor;

    private void Start()
    {
        purchaseButton.onClick.AddListener(BuyElement);
    }

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
                _startPrice = Components.CaseComponentData.BaseBuyPrices[index];
                _increaseFactor = Components.CaseComponentData.RepeatBuyPriceIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(CPUComponent.Name):
                image.sprite = CPUSprites[index];
                _startPrice = Components.CpuComponentData.BaseBuyPrices[index];
                _increaseFactor = Components.CpuComponentData.RepeatBuyPriceIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(GPUComponent.Name):
                image.sprite = GPUSprites[index];
                _startPrice = Components.GpuComponentData.BaseBuyPrices[index];
                _increaseFactor = Components.GpuComponentData.RepeatBuyPriceIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(HDDComponent.Name):
                image.sprite = HDDSprites[index];
                _startPrice = Components.HddComponentData.BaseBuyPrices[index];
                _increaseFactor = Components.HddComponentData.RepeatBuyPriceIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(MBComponent.Name):
                image.sprite = MotherboardSprites[index];
                _startPrice = Components.MBComponentData.BaseBuyPrices[index];
                _increaseFactor = Components.MBComponentData.RepeatBuyPriceIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(PowersupplyComponent.Name):
                image.sprite = PowerSupplySprites[index];
                _startPrice = Components.PowerSupplyComponentData.BaseBuyPrices[index];
                _increaseFactor = Components.PowerSupplyComponentData.RepeatBuyPriceIncreaseFactor[index];
                break;
            case var _ when elementName.Equals(RAMComponent.Name):
                image.sprite = RAMSprites[index];
                _startPrice = Components.RamComponentData.BaseBuyPrices[index];
                _increaseFactor = Components.RamComponentData.RepeatBuyPriceIncreaseFactor[index];
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

    private void BuyElement()
    {
        Element element = Components.GetElementByName(_cardElement.ElementName);

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
        
        // Give feedback if the purchase is done -> instantiate gameObject with text ("+1")
        Instantiate(purchasedTextObject, transform.position, Quaternion.identity, transform);
    }

    private void UpdatePrice()
    {
        float currentPrice = (float)Math.Floor(_startPrice * Math.Pow(_increaseFactor, _cardElement.CountPurchased));
        
        var priceText = GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = $"$ {currentPrice}";
    }
}
