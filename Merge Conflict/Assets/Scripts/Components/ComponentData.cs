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
    private readonly int[] SalePrices; 
    private readonly int[] SaleXP;

    private readonly int[] TrashPrices;

    private readonly int[] BaseBuyPrices;
    private readonly float[] RepeatBuyPriceIncreaseFactor = new float[] { 1.2f, 1.25f, 1.3f, 1.35f };

    public ComponentData(int[] salePrices, int[] saleXP, int[] trashPrices, int[] baseBuyPrices)
    {
        SalePrices = salePrices;
        SaleXP = saleXP;
        TrashPrices = trashPrices;
        BaseBuyPrices = baseBuyPrices;
    }

    public ComponentData(int[] salePrices, int[] saleXP, float salePriceToBaseBuyPriceFactor, float salePriceToTrashPriceFactor = .5f)
    {
        int[] baseBuyPrices = salePrices;
        for (int i = 0; i < baseBuyPrices.Length; i++)
        {
            baseBuyPrices[i] = Mathf.CeilToInt(baseBuyPrices[i] * salePriceToBaseBuyPriceFactor);
        }

        int[] trashPrices = salePrices;
        for (int i = 0; i < trashPrices.Length; i++)
        {
            trashPrices[i] = Mathf.CeilToInt(trashPrices[i] * salePriceToTrashPriceFactor);
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

    public int GetSalePrice(int tier)
    {
        return SalePrices[tier - 1];
    }

    public int GetSaleXP(int tier)
    {
        return SaleXP[tier - 1];
    }

    public int GetTrashPrice(int tier)
    {
        return TrashPrices[tier - 1];
    }

    public int GetBaseBuyPrices(int tier)
    {
        return BaseBuyPrices[tier - 1];
    }

    public float GetRepeatBuyPriceIncreaseFactor(int tier)
    {
        return RepeatBuyPriceIncreaseFactor[tier - 1];
    }
}