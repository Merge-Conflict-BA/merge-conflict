using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        foreach (var foundElement in GameState.Instance.FoundElements)
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
    }
}
