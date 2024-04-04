using UnityEngine;

public class DestroyOnClose : MonoBehaviour
{
    /// <summary>
    /// This method used by the purchasedTextGameObject.
    /// (It is instantiated if the player purchases an element in the element menu. It indicates, that the purchase was successful.)
    /// 
    /// At the end of the used animation there is an event which is calling this method.
    /// </summary>
    public void OnClose()
    {
        Destroy(gameObject);
    }
}
