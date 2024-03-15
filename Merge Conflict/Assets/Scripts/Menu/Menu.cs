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


    public TextMeshProUGUI GetTextfieldByName(string name)
    {
        TextMeshProUGUI textfield = this._canvas.transform.Find(name)?.GetComponent<TextMeshProUGUI>();
        if (textfield == null)
        {
            Debugger.LogError($"The TextMeshPro field with name {name} can't be found in the canvas with the name {this._canvas}");
            return null;
        };

        return textfield;
    }

    public Canvas FindCanvasForMenu(string menuname)
    {
        GameObject foundCanvas = GameObject.Find(menuname);
        return foundCanvas.GetComponent<Canvas>();
    }

    public void SetTitle()
    {
        //get the TextMeshPro field from the title or return 
        TextMeshProUGUI textfield = GetTextfieldByName("Title");
        if (textfield == null) { return; };

        //set the title
        textfield.text = this._canvas.name + " Menu";
    }
}