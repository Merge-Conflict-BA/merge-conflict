/**********************************************************************************************************************
Name:          CardHandler
Description:   Updates the cards with sprite and price for each found element

Author(s):     Simeon Baumann
Date:          2024-03-23
Version:       V1.0
TODO:          - 
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

        // This is to update all prices and not only this single one
        ElementsMenu.Instance.UpdatePurchaseButtons(); // This was previously => UpdatePrice();
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
        MoneyHandler.Instance.SpendMoney((int)Math.Floor(_startPrice * Math.Pow(_increaseFactor, _cardElement.CountPurchased)));
        ElementsMenu.Instance.UpdateActualMoneyText();

        // Update count of purchased elements and the price of the next card
        _cardElement.CountPurchased++;
        FoundElementsHandler.Instance.UpdateStoredElement(_cardElement);
        // This is to update all prices and not only this single one
        ElementsMenu.Instance.UpdatePurchaseButtons(); // This was previously => UpdatePrice();

        // Give feedback if the purchase is done -> instantiate gameObject with text ("+1")
        Instantiate(purchasedTextObject, transform.position, Quaternion.identity, transform);
    }

    public void UpdatePrice()
    {
        float currentPrice = (float)Math.Floor(_startPrice * Math.Pow(_increaseFactor, _cardElement.CountPurchased));

        var priceText = GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = $"$ {currentPrice}";

        // this checks if the player has enough money
        if (currentPrice > MoneyHandler.Instance.Money)
        {
            purchaseButton.interactable = false;
            priceText.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }
        else
        {
            purchaseButton.interactable = true;
            priceText.color = Color.white;
        }
    }
}
