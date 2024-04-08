using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************************************************************************************************************
Name:          ComponentData
Description:   Data Object for Components  

Author(s):     Hanno Witzleb
Date:          2024-04-08
Version:       V1.0
**********************************************************************************************************************/

public class ComponentData
{
    public int[] SalePrices; 
    public int[] SaleXP;

    public int[] TrashPrices;

    public int[] BaseBuyPrices;
    public float[] RepeatBuyPriceIncreaseFactor = new float[] { 1.2f, 1.25f, 1.3f, 1.35f };

    public ComponentData(int[] salePrices, int[] saleXP, int[] trashPrices, int[] baseBuyPrices)
    {
        SalePrices = salePrices;
        SaleXP = saleXP;
        TrashPrices = trashPrices;
        BaseBuyPrices = baseBuyPrices;
    }

    public ComponentData(int[] salePrices, int[] saleXP, float salePriceToBaseBuyPriceFactor, float salePriceToTrashPriceFactor = .5f)
    {
        int[] trashPrices = salePrices;

        for (int i = 0; i < trashPrices.Length; i++)
        {
            trashPrices[i] = Mathf.CeilToInt(trashPrices[i] * salePriceToTrashPriceFactor);
        }

        int[] baseBuyPrices = salePrices;

        for (int i = 0; i < baseBuyPrices.Length; i++)
        {
            baseBuyPrices[i] = Mathf.CeilToInt(baseBuyPrices[i] * salePriceToBaseBuyPriceFactor);
        }

        SalePrices = salePrices;
        SaleXP = saleXP;
        TrashPrices = trashPrices;
        BaseBuyPrices = baseBuyPrices;
    }

    public ComponentData(int[] trashPrices)
    {
        TrashPrices = trashPrices;

        SalePrices = new int[] { };
        SaleXP = new int[] { };
        BaseBuyPrices = new int[] { };
    }
}