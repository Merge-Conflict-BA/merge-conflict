using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Canvas _canvas;

    // public Menu(Canvas menuCanvas)
    // {
    //     this._canvas = menuCanvas;
    //     SetTitle();

    // }



    public void InitializeMenu(Canvas menuCanvas)
    {
        _canvas = menuCanvas;
        SetTitle();
    }


    public T GetComponentByName<T>(string name) where T : Component
    {
        Transform foundGameObject = _canvas.transform.Find(name);
        if (!foundGameObject.TryGetComponent<T>(out var component))
        {
            Debugger.LogError($"The component of type {typeof(T)} with the name {name} can't be found in the canvas {_canvas} !");
            return null;
        }

        return component;
    }

    public Canvas FindCanvasForMenu(string menuname)
    {
        GameObject foundCanvas = GameObject.Find(menuname);
        return foundCanvas.GetComponent<Canvas>();
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