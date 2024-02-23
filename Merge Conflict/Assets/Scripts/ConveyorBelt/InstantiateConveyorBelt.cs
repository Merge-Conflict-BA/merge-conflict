using UnityEngine;

namespace ConveyorBelt
{
    public class InstantiateConveyorBelt : MonoBehaviour
    {
        public static InstantiateConveyorBelt Instance { get; private set; }
    
        private GameObject prefabConveyorBeltHorizontal;
        private GameObject prefabConveyorBeltVertical;

        private Transform parent;

        private void Awake() 
        { 
            // If there is an instance, and it's not me, delete myself.
    
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
    
        public void Instantiate(GameObject _prefabConveyorBeltHorizontal, GameObject _prefabConveyorBeltVertical, Transform _parent)
        {
            prefabConveyorBeltHorizontal = _prefabConveyorBeltHorizontal;
            prefabConveyorBeltVertical = _prefabConveyorBeltVertical;

            parent = _parent;
        
            InstantiatePrefabs();
        }
    
        private void InstantiatePrefabs()
        {
            Vector2 prefabSizeHorizontal = prefabConveyorBeltHorizontal.GetComponent<RectTransform>().rect.size;
            Vector2 prefabSizeVertical = prefabConveyorBeltVertical.GetComponent<RectTransform>().rect.size;

            InitializeConveyorBeltHorizontal(prefabSizeHorizontal);
            InitializeConveyorBeltVertical(prefabSizeVertical, prefabSizeHorizontal.y);
        }


        private void InitializeConveyorBeltHorizontal(Vector2 sizeOfPrefab)
        {
            int screenWidth = Screen.width;
            float prefabWidth = sizeOfPrefab.x;
            Vector2 centerOfPrefab = sizeOfPrefab / 2;
        
            for (int i = 0; (i * prefabWidth) < screenWidth; i++)
            {
                float posX = centerOfPrefab.x + i * prefabWidth;
            
                Vector3 position = new Vector3(posX, centerOfPrefab.y, 0);
                Instantiate(prefabConveyorBeltHorizontal, position, Quaternion.identity, parent);
            }
        }
    
        private void InitializeConveyorBeltVertical(Vector2 sizeOfPrefab, float offsetY)
        {
            int screenHeight = Screen.height;
            float prefabHeight = sizeOfPrefab.y;
            Vector2 centerOfPrefab = sizeOfPrefab / 2;
        
            for (int i = 0; (i * prefabHeight) < screenHeight; i++)
            {
                float posY = centerOfPrefab.y + i * prefabHeight + offsetY;
            
                Vector3 position = new Vector3(centerOfPrefab.x, posY, 0);
                Instantiate(prefabConveyorBeltVertical, position, Quaternion.identity, parent);
            }
        }
    }
}
