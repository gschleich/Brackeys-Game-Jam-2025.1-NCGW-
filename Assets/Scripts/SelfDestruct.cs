using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Call this method to destroy the GameObject
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
