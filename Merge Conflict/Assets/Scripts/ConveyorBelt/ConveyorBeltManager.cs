/**********************************************************************************************************************
Name:          InitializeConveyerBelt
Description:   Initialize Conveyer Belt.
Author(s):     Simeon Baumann
Date:          2024-02-23
Version:       V1.0 
**********************************************************************************************************************/

using UnityEngine;

namespace ConveyorBelt
{
    public class ConveyorBeltManager : MonoBehaviour
    {
        
    
        // Start is called before the first frame update
        void Start()
        {
            ConveyorBelt.Instance.Instantiate();
        }

    

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
