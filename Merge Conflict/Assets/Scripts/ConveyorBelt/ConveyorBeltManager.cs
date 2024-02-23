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
        // Make sure that both Prefabs have the same size
        [SerializeField] public GameObject PrefabConveyorBeltHorizontal;
        [SerializeField] public GameObject PrefabConveyorBeltVertical;
    
        // Start is called before the first frame update
        void Start()
        {
            InstantiateConveyorBelt.Instance.Instantiate(PrefabConveyorBeltHorizontal, PrefabConveyorBeltVertical, gameObject.transform);
        }

    

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
