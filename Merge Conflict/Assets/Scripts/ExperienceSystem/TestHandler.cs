using ExperienceSystem;
using ExpirienceSystem;
using UnityEngine;

public class TestHandler : MonoBehaviour
{
    public LevelTableSO Table;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExperienceHandler.AddExperiencePoints(100);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.LogError(ExperienceHandler.GetExperiencePointsCount());
        }

        if (Input.GetMouseButtonDown(2))
        {
            Debug.LogError(ExperienceHandler.GetCurrentLevel());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ExperienceHandler.ResetCurrentPlayerExperience();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
           var t = ExperienceHandler.GetGameUnlockGameObjects();
            foreach (var obj in t)
            {
                Debug.LogError(obj.name);
            }
        }

        
    }
    
}
