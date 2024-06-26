/**********************************************************************************************************************
Name:          ElementTexture
Description:   Elements data structure for texture.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Own/ElementTexture")]
public class ElementTexture : ScriptableObject
{
    [FormerlySerializedAs("elementSprite")]
    public Sprite sprite;
    public float sizeWidth = 1;
    public float sizeHeight = 1;
    public float sizeScaleX = 1;
    public float sizeScaleY = 1;

    public GameObject ApplyTexture(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out RectTransform _) == false || gameObject.TryGetComponent(out SpriteRenderer _) == false)
        {
            Debugger.LogError("ApplyTexture: gameObject does not have RectTransform or SpriteRenderer attached!!!");
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeWidth, sizeHeight);
        gameObject.GetComponent<RectTransform>().localScale = new Vector2(sizeScaleX, sizeScaleY);

        return gameObject;
    }
}
