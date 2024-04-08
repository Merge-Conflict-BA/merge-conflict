using UnityEngine;

/**********************************************************************************************************************
Name:           ExperienceHandler
Description:    The static class ExperienceHandler provides functions for managing experience points. There is an object 
                LevelTableSO that contains data about various game levels. Inside the "Ressources" folder in Unity, you'll find LevelData where you can set the levels, point counts, and available characters for each level.
Author:         Anna Kozachuk, Markus Haubold 
Date:           2024-03-26
Version:        V1.1 
TODO:           - 
**********************************************************************************************************************/

namespace ExperienceSystem
{
    public static class ExperienceHandler
    {
        private static readonly LevelData[] Levels = new LevelData[]
        {
            new LevelData(1, 0),
            new LevelData(2, 500),
            new LevelData(3, 700),
            new LevelData(4, 1000),
            new LevelData(5, 1300),
            new LevelData(6, 1600),
            new LevelData(7, 2000),
            new LevelData(8, 2500),
            new LevelData(9, 3000),
            new LevelData(10, 3600),
        };

        // returns an integer value of the current level (as it is written in the table)
        public static int GetLevelByExperience(int exp)
        {
            int index = 0;
            int currentLevel;
            do
            {
                currentLevel = Levels[index].Level;
                index++;
                if (index == Levels.Length)
                {
                    return Levels[^1].Level;
                }
            }
            while (Levels[index].RequiredExperience <= exp);

            return currentLevel;
        }        

        public static void AddExperiencePoints(int experiencePoints)
        {
            var currentExp = PlayerPrefs.GetInt("Experience");
            currentExp += experiencePoints;
            PlayerPrefs.SetInt("Experience", currentExp);
        }

        public static int GetExperiencePoints()
        {
            return PlayerPrefs.GetInt("Experience");
        }

        public static int GetCurrentLevel()
        {
            var currentExp = PlayerPrefs.GetInt("Experience");
            var currentLvl = GetLevelByExperience(currentExp);
            return currentLvl;
        }

        public static void ResetCurrentPlayerExperience()
        {
            PlayerPrefs.SetInt("Experience", 0);
        }

        public static int NeededXpToUnlockNextLevel(int currentLevel)
        {
            /*
             * the LevelData array counts from 0...9; so we can use the 
             * current level to get the xp from the upcoming level: current 
             * level=2 => LevelData[2] means value from level 3
            */
            return Levels[currentLevel].RequiredExperience;
        }
    }
}