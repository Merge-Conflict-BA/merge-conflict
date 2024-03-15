using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Canvas _canvas;


    public void InitializeMenu(Canvas menuCanvas)
    {
        _canvas = menuCanvas;
        SetTitle();
    }

    public Canvas FindCanvasForMenu(string menuname)
    {
        GameObject foundCanvas = GameObject.Find(menuname);
        return foundCanvas.GetComponent<Canvas>();
    }

    public T GetComponentByName<T>(string name) where T : Component
    {
        Transform foundGameObject = _canvas.transform.Find(name);
        if (foundGameObject == null)
        {
            Debugger.LogError($"The GameObject with the name {name} can't be found!");
            return null;
        }

        if (!foundGameObject.TryGetComponent<T>(out var component))
        {
            Debugger.LogError($"The component of type {typeof(T)} with the name {name} can't be found in the canvas {_canvas} !");
            return null;
        }

        return component;
    }

    public void SetTitle()
    {
        //get the TextMeshPro field from the title or return 
        TextMeshProUGUI textfield = GetComponentByName<TextMeshProUGUI>("Title");
        if (textfield == null) { return; };

        //set the title
        textfield.text = this._canvas.name + " Menu";
    }
}