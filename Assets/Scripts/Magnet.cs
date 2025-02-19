using UnityEngine;

public class Magnet : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Scrap>(out Scrap scrap))
        {
            scrap.SetTarget(transform.parent.position);
        }
    }
}
