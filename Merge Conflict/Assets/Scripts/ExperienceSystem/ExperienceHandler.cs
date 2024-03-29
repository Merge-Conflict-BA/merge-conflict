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
        private static LevelTableSO _levelsData;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitExperienceHandler()
        {
            if (_levelsData == null)
            {
                _levelsData = Resources.Load<LevelTableSO>("LevelData");
            }
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
            var currentLvl = _levelsData.GetLevelByExperience(currentExp);
            return currentLvl;
        }

        public static void ResetCurrentPlayerExperience()
        {
            PlayerPrefs.SetInt("Experience", 0);
        }
        public static ComponentHandler[] GetGameUnlockPrefabs()
        {
            return _levelsData.GetGameUnlockPrefabs(GetCurrentLevel());
        }

        public static int NeededXpToUnlockNextLevel(int currentLevel)
        {
            /*
             * the LevelData array counts from 0...9; so we can use the 
             * current level to get the xp from the upcoming level: current 
             * level=2 => LevelData[2] means value from level 3
            */
            return _levelsData.Levels[currentLevel].RequirementExperience;
        }
    }
}