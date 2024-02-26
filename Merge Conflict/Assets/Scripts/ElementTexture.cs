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

[CreateAssetMenu(menuName = "Own/ElementTexture")]
public class ElementTexture : ScriptableObject
{
    public string id;
    public string name;
    public Sprite elementSprite;
    public float sizeWidth = 1;
    public float sizeHeight = 1;
    public float sizeScaleX = 1;
    public float sizeScaleY = 1;
}
