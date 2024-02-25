/**********************************************************************************************************************
Name:          PcPart
Description:   Elements data structure.  

Author(s):     Daniel Rittrich
Date:          2024-02-22
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PcPart")]
public class PcPart : ScriptableObject
{
    public string id;
    public string name;
    public int type; // Case: 1     Powersupply: 2     HDD: 3     MB: 4     CPU: 5     RAM: 6     GPU: 7
    public bool isTrash;
    public int itemLevel;
    public int cpu_Slot_lvl;
    public int mb_Slot_lvl;
    public int gpu_Slot_lvl;
    public int ram_Slot_lvl;
    public int hdd_Slot_lvl;
    public int powersupply_Slot_lvl;
    public Sprite elementSprite;
    public float sizeWidth = 1;
    public float sizeHeight = 1;
    public float sizeScaleX = 1;
    public float sizeScaleY = 1;
    public int trashValue;
    public int salesValue;
    public bool canMove; // shout be false when on conveyor belt
    public float movingSpeed = 1;
}