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

    [Header("Start Price for each element")]
    public float[] CaseStartPrice = { 21, 57, 107, 178 };
    public float[] CPUStartPrice = { 64, 171, 321, 535 };
    public float[] GPUStartPrice = { 92, 247, 464, 773 };
    public float[] HDDStartPrice = { 28, 76, 142, 237 };
    public float[] MotherboardStartPrice = { 35, 95, 178, 297 };
    public float[] PowerSupplyStartPrice = { 21, 57, 107, 178 };
    public float[] RAMStartPrice = { 35, 95, 178, 297 };

    [Header("Factor to next price")] 
    public float[] CaseIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public float[] CPUIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public float[] GPUIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public float[] HDDIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public float[] MotherboardIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public float[] PowerSupplyIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public float[] RAMIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };

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

        switch (element.ElementName)
        {
            case ElementName.Case:
                image.sprite = CaseSprites[index];
                _startPrice = CaseStartPrice[index];
                _increaseFactor = CaseIncreaseFactor[index];
                break;
            case ElementName.CPU:
                image.sprite = CPUSprites[index];
                _startPrice = CPUStartPrice[index];
                _increaseFactor = CPUIncreaseFactor[index];
                break;
            case ElementName.GPU:
                image.sprite = GPUSprites[index];
                _startPrice = GPUStartPrice[index];
                _increaseFactor = GPUIncreaseFactor[index];
                break;
            case ElementName.HDD:
                image.sprite = HDDSprites[index];
                _startPrice = HDDStartPrice[index];
                _increaseFactor = HDDIncreaseFactor[index];
                break;
            case ElementName.Motherboard:
                image.sprite = MotherboardSprites[index];
                _startPrice = MotherboardStartPrice[index];
                _increaseFactor = MotherboardIncreaseFactor[index];
                break;
            case ElementName.PowerSupply:
                image.sprite = PowerSupplySprites[index];
                _startPrice = PowerSupplyStartPrice[index];
                _increaseFactor = PowerSupplyIncreaseFactor[index];
                break;
            case ElementName.RAM:
                image.sprite = RAMSprites[index];
                _startPrice = RAMStartPrice[index];
                _increaseFactor = RAMIncreaseFactor[index];
                break;
            case ElementName.Default:
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
        
        element.InstantiateGameObjectAndAddTexture(GetRandomPositionOnDesk());
        
        // Update count of purchased elements and the price of the next card
        _cardElement.CountPurchased++;
        FoundElementsHandler.Instance.UpdateCountPurchased(_cardElement);
        UpdatePrice();
    }

    private void UpdatePrice()
    {
        float currentPrice = (float)Math.Floor(_startPrice * Math.Pow(_increaseFactor, _cardElement.CountPurchased));
        
        var priceText = GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = $"$ {currentPrice}";
    }
    
    private Vector2 GetRandomPositionOnDesk()
    {
        GameObject deskObject = GameObject.FindWithTag("desk");
        RectTransform rectTransform = deskObject.GetComponent<RectTransform>();
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        int padding = 100;

        float x = Random.Range(anchoredPosition.x + padding, anchoredPosition.x + rectTransform.rect.width - padding);
        float y = Random.Range(anchoredPosition.y + padding, anchoredPosition.y + rectTransform.rect.height - padding);

        return new Vector2(x, y);
    }
}
