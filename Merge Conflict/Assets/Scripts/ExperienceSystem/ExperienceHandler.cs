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

        private static readonly string PlayerPrefsKey = "Experience";

        public static int GetLevelByTotalXP(int totalXP)
        {
            int xp = totalXP;
            int levelIndex = 0;
            
            while (levelIndex < Levels.Length - 1
                && xp >= Levels[levelIndex + 1].RequiredExperience)
            {
                xp -= Levels[levelIndex + 1].RequiredExperience;
                levelIndex++;
            }

            return Levels[levelIndex].Level;
        }

        public static void AddExperiencePoints(int experiencePoints)
        {
            var currentExp = PlayerPrefs.GetInt(PlayerPrefsKey);
            currentExp += experiencePoints;

            PlayerPrefs.SetInt(PlayerPrefsKey, currentExp);
        }

        public static int GetExperiencePointsInCurrentLevel()
        {
            int currentTotalXP = PlayerPrefs.GetInt(PlayerPrefsKey);
            int currentLevel = GetLevelByTotalXP(currentTotalXP);

            int currentXPInLevel = currentTotalXP;
            for (int i = 1; i < currentLevel; i++)
            {
                currentXPInLevel -= Levels[i].RequiredExperience;
            }

            return currentXPInLevel;
        }

        public static int GetCurrentLevel()
        {
            int currentTotalXP = PlayerPrefs.GetInt(PlayerPrefsKey);
            return GetLevelByTotalXP(currentTotalXP);            
        }

        public static void ResetCurrentPlayerExperience()
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, 0);
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