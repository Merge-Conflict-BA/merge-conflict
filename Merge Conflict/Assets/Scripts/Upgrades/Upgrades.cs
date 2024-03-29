/**********************************************************************************************************************
Name:          Upgrades
Description:   Contains all upgrades
Author(s):     Hanno Witzleb
Date:          2024-03-29
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Upgrades
{
    public static Upgrade[] List = new Upgrade[]{
        new Upgrade("Example", new int[]{300, 400, 500, 700})
    };
}
