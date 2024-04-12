/**********************************************************************************************************************
Name:          JSONElement
Description:   Data structure und functions for saving and loading element (components) to PlayerPrefs.  
Author(s):     Hanno Witzleb
Date:          2024-04-11
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedElement {
    public readonly string Name;
    public readonly int Tier; // also used for trashVariant

    public readonly List<SavedElement> Children;

    public SavedElement(string name, int tier, List<SavedElement> children = null) {
        Name = name;
        Tier = tier;

        Children = children ?? new List<SavedElement>();
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

        foreach(SavedElement child in Children){
            element += child.Serialize(childLevel + 1);
        }

        return element;
    }

    // Example SavedElement: "Case,1|HDD,1|PowerSupply,1|Motherboard,1?CPU,1?GPU,1?RAM,1"    
    public static SavedElement Deserialize(string element, int childLevel = 0){
        string[] elements = element.Split(GetChildSeperator(childLevel + 1));

        string[] parentElementField = elements[0].Split(GetFieldSeperator());

        SavedElement savedElement = new(parentElementField[0], int.Parse(parentElementField[1]));

        if(elements.Length == 1){
            return savedElement;
        }

        for(int i = 1; i < elements.Length; i++) {
            savedElement.Children.Add(Deserialize(elements[i], childLevel + 1));
        }

        return savedElement;
    }

    public override string ToString()
    {
        string s = $"{Name}, {Tier}";

        foreach (SavedElement child in Children)
        {
            s += $"-child: {child}\n";
        }

        return s;
    }
}
