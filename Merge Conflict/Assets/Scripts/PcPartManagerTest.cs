using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PcPartManagerTest : MonoBehaviour
{
    public PcPart data;
    public GameObject objectCPU;
    public GameObject objectRAM;
    public GameObject objectGPU;



    private int counter = 0;

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
                counter += 1;
                Debug.Log("Test");
                CheckAndDuplicate();
            }
        }
    }

    private void CheckAndDuplicate()
    {
        GameObject duplicate = Instantiate(counter == 1 ? objectCPU : counter == 2 ? objectRAM : objectGPU, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        duplicate.transform.SetParent(gameObject.transform, true);
        duplicate.name = gameObject.name + (counter == 1 ? "_objectCPU" : counter == 2 ? "_objectRAM" : "_objectGPU");

        SpriteRenderer gameObjectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer duplicateSpriteRenderer = duplicate.GetComponent<SpriteRenderer>();
        duplicateSpriteRenderer.sortingOrder = gameObjectSpriteRenderer.sortingOrder + 1;
    }
}
