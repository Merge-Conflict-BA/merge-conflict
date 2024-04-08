using System;
/**********************************************************************************************************************
Name:          LevelData
Description:   This class is used together with LevelTableSO (which contains an array of LevelData objects) to store data on various game levels, including information on the required experience and related game components.
Author:    Anna Kozachuk
Date:          2024-03-11
**********************************************************************************************************************/

namespace ExperienceSystem
{
    public class LevelData
    {
        public int Level;
        public int RequiredExperience;

        public LevelData(int level, int requiredExperience)
        {
            Level = level;
            RequiredExperience = requiredExperience;
        }
    }
}