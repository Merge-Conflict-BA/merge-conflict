using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Canvas _canvas;

    public Menu(Canvas menuCanvas)
    {
        this._canvas = menuCanvas;
        SetTitle();

    }

    private void SetTitle()
    {
        //find the TextMeshProUGUI from the title
        TextMeshProUGUI menuTitle = this._canvas.transform.Find("Title")?.GetComponent<TextMeshProUGUI>();
        if(menuTitle == null) 
        {
            Debugger.LogMessage("TextMeshPro object with name 'Title' not found in the canvas." + this._canvas);
            return; 
        };

        //set the title
        menuTitle.text = this._canvas.name;
    }
}