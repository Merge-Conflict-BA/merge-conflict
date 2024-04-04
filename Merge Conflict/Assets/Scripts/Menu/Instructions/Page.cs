using UnityEngine;

public class Page
{
    public int Id { private set; get; }
    public GameObject gameObject { private set; get; }
    public string PageObjectName { private set; get; }

    public Page(int id, GameObject pageGameObject)
    {
        this.Id = id;
        this.gameObject = pageGameObject;
        this.PageObjectName = pageGameObject.name;
    }
}