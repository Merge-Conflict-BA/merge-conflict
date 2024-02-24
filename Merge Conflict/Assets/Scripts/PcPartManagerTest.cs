using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PcPartManagerTest : MonoBehaviour
{
    public PcPart data;


    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = data.ElementSprite;

        rectTransform.sizeDelta = new Vector2(data.SizeWidth, data.SizeHeight);
        transform.localScale = new Vector2(data.ScaleX, data.ScaleY);
        boxCollider2D.isTrigger = true;
        boxCollider2D.size = new Vector2(data.SizeBoxColliderWidth, data.SizeBoxColliderHeight);
    }

    void Update()
    {

    }

}
