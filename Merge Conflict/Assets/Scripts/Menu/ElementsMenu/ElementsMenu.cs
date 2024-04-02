/**********************************************************************************************************************
Name:          ElementsMenu
Description:   Menu of all found elements. There is an option to buy it 

Author(s):     Simeon Baumann
Date:          2024-03-23
Version:       V1.0
**********************************************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementsMenu : Menu
{
    public GameObject CardPrefab;
    public GameObject CardsListContentObject;
    private GameObject[] FoundElementCardObjects;

    private Vector2 _displayedCardSize;
    
    #region Singleton
    private static ElementsMenu _instance;
    public static ElementsMenu Instance { get { return _instance; } }
        
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
    #endregion

    public void OpenMenu()
    {
        CardsListContentObject.SetActive(true);

        InitializeMenu(gameObject.GetComponent<Canvas>());
        UpdateContentViewport();
        InstantiateCardObjects();
    }

    private void UpdateContentViewport()
    {
        // Calculate CellSize for the GridLayoutGroup of the CardsListContentObject to handle different screen resolution
        // so the Cards are displayed all in the center
        
        // Get sizes and GridLayoutGroup from CardsListContentObject
        RectTransform contentRectTransform = CardsListContentObject.GetComponent<RectTransform>();
        float width = contentRectTransform.rect.size.x - contentRectTransform.sizeDelta.x;

        GridLayoutGroup gridLayoutGroup = CardsListContentObject.GetComponent<GridLayoutGroup>();
        float xSpacing = gridLayoutGroup.spacing.x;

        int columnCount = gridLayoutGroup.constraintCount;
        Vector2 cellSize = Vector2.zero;

        cellSize.x = (width - (xSpacing * (columnCount - 1))) / columnCount;

        // Calculate cellHeight by using the size of the CardPrefab
        RectTransform cardRectTransform = CardPrefab.GetComponent<RectTransform>();
        Vector2 sizeOfCard = cardRectTransform.rect.size;
        cellSize.y = (sizeOfCard.y * cellSize.x) / sizeOfCard.x;

        // Set new calculated cellSize to the GridLayoutGroup
        gridLayoutGroup.cellSize = cellSize;
        _displayedCardSize = cellSize;
    }

    private void InstantiateCardObjects()
    {
        // get all current instantiated cards as a list<string>
        List<string> cardNames = new List<string>();
        foreach (Transform cardTransform in CardsListContentObject.transform)
        {
            cardNames.Add(cardTransform.gameObject.name);
        }
        
        // Instantiate all found elements if they aren't instantiated yet
        foreach (var foundElement in FoundElementsHandler.Instance.GetFoundElements())
        {
            string cardTitle = foundElement.ToCardTitle();

            if (cardNames.Contains(cardTitle))
            {
                continue;
            }
            
            GameObject cardObject = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity, CardsListContentObject.transform);
            cardObject.name = cardTitle;
            
            // Update the Sprite so that the right element is displayed on the card
            CardHandler cardHandler = cardObject.GetComponent<CardHandler>();
            cardHandler.UpdateSprite(foundElement);

            // Set right size for boxCollider2D
            BoxCollider2D boxCollider2D = cardObject.GetComponent<BoxCollider2D>();
            boxCollider2D.size = _displayedCardSize;
        }
        
        // Reorder cards
        SortCards();
    }

    private void SortCards()
    {
        List<Transform> cardObjects = CardsListContentObject.transform.Cast<Transform>().ToList();

        List<Transform> orderedCardObjects = cardObjects.OrderBy(card => card.name).ToList();

        for (int i = 0; i < orderedCardObjects.Count; i++)
        {
            orderedCardObjects[i].SetSiblingIndex(i);
        }
    }

    public void CloseMenu()
    {
        CardsListContentObject.SetActive(false);
    }
}
