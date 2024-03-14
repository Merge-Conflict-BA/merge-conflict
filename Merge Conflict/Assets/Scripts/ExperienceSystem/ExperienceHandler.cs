using ExperienceSystem;
using UnityEngine;

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
    }
}
