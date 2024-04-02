/**********************************************************************************************************************
Name:           OrderComponents
Description:    Contains all static Parameters for Order Generation
Author(s):      Markus Haubold, Hanno Witzleb
Date:           2024-03-27
Version:        V2.0
TODO:           - 
**********************************************************************************************************************/


public enum OrderComponentIndex
{
    Case = 0,
    HDD = 1,
    Powersupply = 2,
    Motherboard = 3,
    GPU = 4,
    CPU = 5,
    RAM = 6
}

public enum Tiers
{
    T1 = 0,
    T2 = 1,
    T3 = 2,
    T4 = 3
}

public static class OrderComponents
{
    public static OrderComponent[] List = new OrderComponent[]
    {
        new OrderComponent(
            nameof(OrderComponentIndex.Case),
            new int[] { 1, 2, 4, 6 },
            new int[] { 1, 2, 3, 4 }
            ),
        new OrderComponent(
            nameof(OrderComponentIndex.HDD),
            new int[] { 1, 3, 5, 7 },
            new int[] { 1, 2, 3, 4 }
            ),
        new OrderComponent(
            nameof(OrderComponentIndex.Powersupply),
            new int[] { 1, 4, 6, 8 },
            new int[] { 1, 2, 3, 4 }
            ),
        new OrderComponent(
            nameof(OrderComponentIndex.Motherboard),
            new int[] { 1, 5, 7, 9 },
            new int[] { 1, 2, 3, 4 }
            ),
        new OrderComponent(
            nameof(OrderComponentIndex.GPU),
            new int[] { 1, 6, 8, 10 },
            new int[] { 1, 2, 3, 4 }
            ),
        new OrderComponent(
            nameof(OrderComponentIndex.CPU),
            new int[] { 1, 6, 8, 10 },
            new int[] { 1, 2, 3, 4 }
            ),
        new OrderComponent(
            nameof(OrderComponentIndex.RAM),
            new int[] { 1, 7, 9, 10 },
            new int[] { 1, 2, 3, 4 }
            )
    };
}
