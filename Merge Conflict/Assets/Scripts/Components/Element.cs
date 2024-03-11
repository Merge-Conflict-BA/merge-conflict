/**********************************************************************************************************************
Name:          Element
Description:   Elements data structure.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    /*  // caused stackoverflow error
    public int level
    {
        get { return level; }
        set { level = Mathf.Clamp(level, 1, 4); }
    } */

    private int _level;
    public int level
    {
        get { return _level; }
        set { _level = Mathf.Clamp(value, 1, 4); }
    }
    public int trashValue;
    public int salesValue;

    public Element(int level, int trashValue, int salesValue)
    {
        this.level = level;
        this.trashValue = trashValue;
        this.salesValue = salesValue;
    }
}