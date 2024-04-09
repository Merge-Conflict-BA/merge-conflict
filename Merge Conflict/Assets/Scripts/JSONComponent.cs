/**********************************************************************************************************************
Name:          JSONComponent
Description:   Data structure for saving element (components) to PlayerPrefs.  
Author(s):     Daniel Rittrich
Date:          2024-04-09
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JSONComponent
{
    public int tier;
    public string name;
    public int variant;

    public JSONComponent? powersupply;
    public JSONComponent? hdd;
    public JSONComponent? motherboard;
    public JSONComponent? cpu;
    public JSONComponent? ram;
    public JSONComponent? gpu;


    public JSONComponent(int tier, string name, JSONComponent? powersupply = null, JSONComponent? hdd = null, JSONComponent? motherboard = null, JSONComponent? cpu = null, JSONComponent? ram = null, JSONComponent? gpu = null)
    {
        this.tier = tier;
        this.name = name;
        this.powersupply = powersupply;
        this.hdd = hdd;
        this.motherboard = motherboard;
        this.cpu = cpu;
        this.ram = ram;
        this.gpu = gpu;
    }

    public JSONComponent(int tier, string name)
    {
        this.tier = tier;
        this.name = name;
    }

    public JSONComponent(string name, int variant)
    {
        this.name = name;
        this.variant = variant;
    }

    public static JSONComponent EmptyJSONComponent = new(
    tier: 0,
    name: "empty");

}