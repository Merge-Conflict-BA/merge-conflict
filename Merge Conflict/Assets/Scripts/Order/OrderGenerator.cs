/**********************************************************************************************************************
Name:           OrderGenerator
Description:    Create a new order with 7 components and the corresponding level. The level is selected with a 
                probability which is calculated based on how long ago the respective level was unlocked. This means that
                a level that has only recently been unlocked will be more likely to be filled than a component that has 
                been unlocked for several levels.  
Author(s):      Markus Haubold
Date:           2024-03-26
Version:        V1.1
TODO:           - 
**********************************************************************************************************************/

using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }
    public Order? Order = null;

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {            
#if UNITY_EDITOR
            WriteDataLogFile();
#endif

            _instance = this;
        }
    }

    void Start()
    {
        
    }

    public Order GenerateNewOrder(int currentLevel)
    {
        int[] orderComponentTier = new int[OrderComponents.List.Length];
        for (int componentIndex = 0; componentIndex < OrderComponents.List.Length; componentIndex++)
        {
            orderComponentTier[componentIndex] = SelectTierForComponent((OrderComponentIndex)componentIndex, currentLevel);
        }

        int caseTier = orderComponentTier[(int)OrderComponentIndex.Case];
        int hddTier = orderComponentTier[(int)OrderComponentIndex.HDD];
        int powersupplyTier = orderComponentTier[(int)OrderComponentIndex.Powersupply];
        int motherboardTier = orderComponentTier[(int)OrderComponentIndex.Motherboard];
        int gpuTier = orderComponentTier[(int)OrderComponentIndex.GPU];
        int cpuTier = orderComponentTier[(int)OrderComponentIndex.CPU];
        int ramTier = orderComponentTier[(int)OrderComponentIndex.RAM];

        Order order = new Order(caseTier, hddTier, powersupplyTier, motherboardTier, gpuTier, cpuTier, ramTier);
        Order = order;
       
        return order;
    }

    public void DeleteOrder()
    {
        Order = null;
    }

    /*
     * calculate the probabilities for Tier1...Tier4
     * generate an random number between 0 and 1
     * choose the Tier into which the randomly selected number fits
    */
    private int SelectTierForComponent(OrderComponentIndex orderComponentIndex, int currentLevel)
    {
        OrderComponent orderComponent = OrderComponents.List[(int)orderComponentIndex];
        float[] probabilityTierIsInOrder = orderComponent.CalculateProbabilities(currentLevel);

        float p1 = probabilityTierIsInOrder[(int)Tiers.T1];
        float p2 = probabilityTierIsInOrder[(int)Tiers.T2];
        float p3 = probabilityTierIsInOrder[(int)Tiers.T3];
        float p4 = probabilityTierIsInOrder[(int)Tiers.T4];

        float cumulativeP1 = p1;
        float cumulativeP2 = cumulativeP1 + p2;
        float cumulativeP3 = cumulativeP2 + p3;
        float cumulativeP4 = cumulativeP3 + p4;

        float randomNumber = UnityEngine.Random.value;
        return randomNumber switch
        {
            float n when n < cumulativeP1 => 1,
            float n when n < cumulativeP2 => 2,
            float n when n < cumulativeP3 => 3,
            float n when n < cumulativeP4 => 4,
            _ => 0 //if the randomNumber fits with nothing, 0 will be return and the Component class should run into an error
        };
    }

    /*
     * every time this class will be initialized, all parameters and all calculated values will be logged for all components for all levels
     * this can be usefull for the process of tune the probabilities because it gives an complete overview 
    */

    private void WriteDataLogFile()
    {
        const string filePath = "Assets/Scripts/Order/dataLog.txt";
        const int levels = 10;
        DateTime currentDateAndTime = DateTime.Now;

        using StreamWriter writer = new StreamWriter(filePath);

        writer.WriteLine("All data which are needed to generate the Order.");
        writer.WriteLine("To understand what they data are used for, please checkout the Order/README.md!");
        writer.WriteLine($"Generated: {currentDateAndTime.Day}.{currentDateAndTime.Month}.{currentDateAndTime.Year} at {currentDateAndTime.Hour}:{currentDateAndTime.Minute}:{currentDateAndTime.Second}");
        writer.WriteLine(" ");

        foreach (var orderComponent in OrderComponents.List)
        {
            writer.Write(orderComponent.ToString());
            writer.WriteLine("");

            for (int level = 1; level <= levels; level++)
            {
                writer.Write(orderComponent.GetDebugProbabilities(level));
            }

            writer.WriteLine("");
        }
    }
}