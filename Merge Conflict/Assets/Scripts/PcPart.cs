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
    public string ID;
    public string Name;
    public bool IsTrash;
    public int ItemLevel;
    public int CPU_Slot_lvl;
    public int MB_Slot_lvl;
    public int GPU_Slot_lvl;
    public int RAM_Slot_lvl;
    public int HDD_Slot_lvl;
    public int Powersupply_Slot_lvl;
    public Sprite ElementSprite;
    public float SizeWidth = 1;
    public float SizeHeight = 1;
    public float ScaleX = 1;
    public float ScaleY = 1;
    public int TrashValue;
    public int SalesValue;
    public bool CanMove; // shout be false when on conveyor belt

}