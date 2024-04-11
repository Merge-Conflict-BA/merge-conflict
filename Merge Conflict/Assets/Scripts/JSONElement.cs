/**********************************************************************************************************************
Name:          JSONElement
Description:   Data structure und functions for saving and loading element (components) to PlayerPrefs.  
Author(s):     Hanno Witzleb
Date:          2024-04-11
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JSONElement {
    public string Name;
    public int Tier; // also used for trashVariant

    public List<JSONElement> Children;

    public JSONElement(string name, int tier, List<JSONElement> children = null) {
        Name = name;
        Tier = tier;

        if(children == null){            
            Children = new List<JSONElement>();
        } else {
            Children = children;
        }
    }

    private static string GetFieldSeperator(){
        return ",";
    }

    private static string GetChildSeperator(int level) {
        return level switch
        {
            1 => "|",
            2 => "?",
            3 => "*",
            _ => "ERROR",
        };
    }

    public string Serialize(int childLevel = 0){
        string element = "";

        if(childLevel > 0){
            element+= GetChildSeperator(childLevel);
        }

        element += Name;
        element += GetFieldSeperator();
        element += Tier.ToString();

        if(Children == null){
            return element;
        }

        foreach(JSONElement child in Children){
            element += child.Serialize(childLevel + 1);
        }

        return element;
    }

    // Example JSON: "Case,1|HDD,1|PowerSupply,1|Motherboard,1?CPU,1?GPU,1?RAM,1"    

    public static JSONElement Deserialize(string element, int childLevel = 0){
        string[] elements = element.Split(GetChildSeperator(childLevel + 1));

        string[] parentElementField = elements[0].Split(GetFieldSeperator());

        JSONElement jsonElement = new(parentElementField[0], int.Parse(parentElementField[1]));

        if(elements.Length == 1){
            return jsonElement;
        }

        for(int i = 1; i < elements.Length; i++) {
            jsonElement.Children.Add(Deserialize(elements[i], childLevel + 1));
        }

        return jsonElement;
    }
}
