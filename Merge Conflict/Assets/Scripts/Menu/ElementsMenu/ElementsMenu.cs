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

public class ElementsMenu : Menu
{
    public GameObject CardPrefab;
    public GameObject CardsListContentObject;
    
    private Canvas _elementsmenuCanvas;
    private GameObject[] FoundElementCardObjects;
    
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
        _elementsmenuCanvas = FindCanvasForMenu("Elements");

        InitializeMenu(_elementsmenuCanvas);
        InstantiateCardObjects();
    }

    private void InstantiateCardObjects()
    {
        // get all current instantiated cards as a list<string>
        List<string> cards = new List<string>();
        foreach (Transform card in CardsListContentObject.transform)
        {
            cards.Add(card.gameObject.name);
        }
        
        // Instantiate all found elements if they aren't instantiated yet
        foreach (var foundElement in FoundElementsHandler.Instance.GetFoundElements())
        {
            string cardTitle = foundElement.ToCardTitle();

            if (cards.Contains(cardTitle))
            {
                continue;
            }
            
            GameObject cardObject = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity, CardsListContentObject.transform);
            cardObject.name = cardTitle;
            
            CardHandler cardHandler = cardObject.GetComponent<CardHandler>();
            cardHandler.UpdateSprite(foundElement);
        }
        
        // Reorder cards
        SortCards();
    }

    private void SortCards()
    {
        List<Transform> cardObjects = CardsListContentObject.transform.Cast<Transform>().ToList();

        List<Transform> orderedCardObjects = cardObjects.OrderBy(p => p.name).ToList();

        for (int i = 0; i < orderedCardObjects.Count; i++)
        {
            orderedCardObjects[i].SetSiblingIndex(i);
        }
    }
}
