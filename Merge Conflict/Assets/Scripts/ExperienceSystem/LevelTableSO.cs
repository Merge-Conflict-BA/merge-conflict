using UnityEngine;

namespace ExperienceSystem
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelTableSO : ScriptableObject
    {
        public LevelData[] LevelsDescription;

        public int GetLevelByExperience(int exp)
        {
            int currentLevel = 0;
            int index = 0;
            do
            {
                currentLevel = LevelsDescription[index].Level;
                index++;
                if (index == LevelsDescription.Length)
                {
                    return LevelsDescription[^1].Level;
                }
            }
            while (LevelsDescription[index].RequirementExperience <= exp);
            
            return currentLevel;
        }

        public ComponentHandler[] GetGameUnlockPrefabs(int level)
        {
            if (level > LevelsDescription.Length) level = LevelsDescription.Length;
            ComponentHandler[] unlockedGameObjects = new ComponentHandler[level];
            for (int i = 0; i < level; i++)
            {
                unlockedGameObjects[i] = LevelsDescription[i].PartGameObject;
            }

            return unlockedGameObjects;
        }
    }
}