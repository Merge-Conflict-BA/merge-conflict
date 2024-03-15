using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentParameter
{
    Dictionary<string, int> evolutionstageUnlocklevel = new Dictionary<string, int>();
    Dictionary<string, int> distanceMultiplier = new Dictionary<string, int>(); //scale with this the probability; by default it is the number from the evolution stage (1...4)

    public ComponentParameter(int evolutionstage1Unlocklevel, int evolutionstage2Unlocklevel, int evolutionstage3Unlocklevel, int evolutionstage4Unlocklevel)
    {
        evolutionstageUnlocklevel.Add("stage1", evolutionstage1Unlocklevel);    
        evolutionstageUnlocklevel.Add("stage2", evolutionstage2Unlocklevel);
        evolutionstageUnlocklevel.Add("stage3", evolutionstage3Unlocklevel);
        evolutionstageUnlocklevel.Add("stage4", evolutionstage4Unlocklevel);

        distanceMultiplier.Add("evolutionstage1", 1);
        distanceMultiplier.Add("evolutionstage2", 2);
        distanceMultiplier.Add("evolutionstage3", 3);
        distanceMultiplier.Add("evolutionstage4", 4);
    }


    

    public void SetDistanceMultiplier(string evolutionstage, int distance)
    {
        distanceMultiplier[evolutionstage] = distance;
    }
}
