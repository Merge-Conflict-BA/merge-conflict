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

    public void UpdateSprite(FoundElement foundElement)
    {
        _cardElement = foundElement;

        Image image = GetComponent<Image>();

        int elementTier = foundElement.Level - 1;
        string elementName = foundElement.ElementName;

        Element element = Components.GetElementByName(elementName);

        _startPrice = element.GetComponentData().BaseBuyPrices[elementTier];
        _increaseFactor = element.GetComponentData().RepeatBuyPriceIncreaseFactor[elementTier];

        switch (elementName)
        {
            case var _ when elementName.Equals(CaseComponent.Name):
                image.sprite = CaseSprites[elementTier];
                break;

            case var _ when elementName.Equals(CPUComponent.Name):
                image.sprite = CPUSprites[elementTier];
                break;

            case var _ when elementName.Equals(GPUComponent.Name):
                image.sprite = GPUSprites[elementTier];
                break;

            case var _ when elementName.Equals(HDDComponent.Name):
                image.sprite = HDDSprites[elementTier];
                break;

            case var _ when elementName.Equals(MBComponent.Name):
                image.sprite = MotherboardSprites[elementTier];
                break;

            case var _ when elementName.Equals(PowersupplyComponent.Name):
                image.sprite = PowerSupplySprites[elementTier];
                break;

            case var _ when elementName.Equals(RAMComponent.Name):
                image.sprite = RAMSprites[elementTier];
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
        Element element = Components.GetElementByName(_cardElement.ElementName);

        if (element == null)
        {
            Debugger.LogError("Could not get Component from Card element.");
            AudioManager.Instance.PlayErrorSound();
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
        AudioManager.Instance.PlayBuyElementButtonSound();
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
