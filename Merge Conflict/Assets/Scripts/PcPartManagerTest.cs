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

        spriteRenderer.sprite = data.ElementSprite;

        rectTransform.sizeDelta = new Vector2(data.SizeWidth, data.SizeHeight);
        transform.localScale = new Vector2(data.ScaleX, data.ScaleY);

    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Test");
                CheckAndDuplicate();
            }
        }
    }

    private void CheckAndDuplicate()
    {
        GameObject duplicate = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        duplicate.name = gameObject.name + "_duplicate";

        duplicate.transform.SetParent(gameObject.transform, true);
    }
}
