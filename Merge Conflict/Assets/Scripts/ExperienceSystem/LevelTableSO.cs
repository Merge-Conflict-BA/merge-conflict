using UnityEngine;

/**********************************************************************************************************************
Name:          LevelTableSO
Description:   It contains data about various game levels. Inside the "Ressources" folder in Unity, you'll find LevelData where you can set the levels, point counts, and available characters for each level.
Author:    Anna Kozachuk
Date:          2024-03-11
TODO:          already discussed with Markus, how he gets the data
**********************************************************************************************************************/

namespace ExperienceSystem
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelTableSO : ScriptableObject
    {
        public LevelData[] Levels;

/*The first method (GetLevelByExperience) returns an integer value of the current level (as it is written in the table)*/

        public int GetLevelByExperience(int exp)
        {
            int currentLevel = 0;
            int index = 0;
            do
            {
                currentLevel = Levels[index].Level;
                index++;
                if (index == Levels.Length)
                {
                    return Levels[^1].Level;
                }
            }
            while (Levels[index].RequirementExperience <= exp);
            
            return currentLevel;
        }
/*The second method (GetGameUnlockPrefabs) returns an array of the objects available at the current state (which are available at this and all previous levels)*/
        public ComponentHandler[] GetGameUnlockPrefabs(int level)
        {
            level++;
            if (level > Levels.Length) level = Levels.Length;
            ComponentHandler[] unlockedGameObjects = new ComponentHandler[level];
            for (int i = 0; i < level; i++)
            {
                unlockedGameObjects[i] = Levels[i].PartGameObject;
            }

            return unlockedGameObjects;
        }
    }
}