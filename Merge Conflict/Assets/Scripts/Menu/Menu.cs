using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Canvas _canvas;
    const string TitleObjectName = "Title";


    public void InitializeMenu(Canvas canvas)
    {
        _canvas = canvas;
        SetTitle();
    }

    public Canvas FindCanvasForMenu(string name)
    {
        GameObject foundCanvas = GameObject.Find(name);
        return foundCanvas.GetComponent<Canvas>();
    }

    public T GetComponentbyObjectName<T>(string name) where T : Component
    {
        Transform foundGameObject = _canvas.transform.Find(name);
        if (foundGameObject == null)
        {
            Debugger.LogError($"GetComponentByName: The GameObject with the name {name} can't be found!");
            return null;
        }

        if (!foundGameObject.TryGetComponent<T>(out var component))
        {
            Debugger.LogError($"GetComponentByName: The component of type {typeof(T)} with the name {name} can't be found in the canvas {_canvas} !");
            return null;
        }

        return component;
    }

    public void SetTitle()
    {
        //get the TextMeshPro field from the title or return 
        TextMeshProUGUI textfield = GetComponentbyObjectName<TextMeshProUGUI>(TitleObjectName);
        if (textfield == null) { return; };

        //set the title
        textfield.text = this._canvas.name + " Menu";
    }
}