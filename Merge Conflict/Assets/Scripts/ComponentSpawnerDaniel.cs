using UnityEngine;

public class ComponentSpawnerDaniel : MonoBehaviour
{
    private static ComponentSpawnerDaniel _instance;
    public static ComponentSpawnerDaniel Instance { get { return _instance; } }

    public GameObject baseObjectToSpawn;


    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SpawnObject(Element element)
    {
        GameObject newObject = Instantiate(baseObjectToSpawn, element.transform.position, element.transform.rotation);

        newObject.name = element.name + "_newObject";

        SpriteRenderer newObjectSpriteRenderer = newObject.GetComponent<SpriteRenderer>();
        RectTransform newObjectRectTransform = newObject.GetComponent<RectTransform>();
        BoxCollider2D newObjectBoxCollider2D = newObject.GetComponent<BoxCollider2D>();

        ElementTexture newObjectTexture = TextureAtlas.Instance.getTexture(element);

        newObjectSpriteRenderer.sprite = newObjectTexture.elementSprite;

        newObjectRectTransform.sizeDelta = new Vector2(newObjectTexture.sizeWidth, newObjectTexture.sizeHeight);
        newObjectRectTransform.localScale = new Vector2(newObjectTexture.sizeScaleX, newObjectTexture.sizeScaleY);
        newObjectBoxCollider2D.isTrigger = true;
        newObjectBoxCollider2D.size = new Vector2(newObjectTexture.sizeWidth, newObjectTexture.sizeHeight);


        // @ Daniel   TODO: implement logic for equiped components on MB
        /* if (element >= 1)
        {
            GameObject newChildObject = Instantiate(baseObjectToSpawn, spawnPosition, rotation, newObject.transform.parent);
            newChildObject.transform.SetParent(newObject.transform, true);
            newChildObject.name = newObject.name + "_newChildObject";
            SpriteRenderer newChildObjectSpriteRenderer = newChildObject.GetComponent<SpriteRenderer>();
            newChildObjectSpriteRenderer.sortingOrder = newObjectSpriteRenderer.sortingOrder + 1;

            ElementTexture newChildObjectTexture = TextureAtlas.Instance.getEquipedTexture()
        }
        if (levelOfEquipedComponent >= 1 &&)
        {

        } */

    }
}
