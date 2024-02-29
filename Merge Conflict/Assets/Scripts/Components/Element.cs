/**********************************************************************************************************************
Name:          Element
Description:   Elements data structure.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public string id;
    public new string name;
    public int type; // Case: 0     Powersupply: 1     HDD: 2     MB: 3     CPU: 4     RAM: 5     GPU: 6     Trash: 7
    public int level;
    public int trashValue;
    public int salesValue;
    public bool canMove; // shout be false when on conveyor belt
}