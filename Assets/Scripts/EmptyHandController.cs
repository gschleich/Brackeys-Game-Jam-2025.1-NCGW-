using UnityEngine;

public class EmptyHandController : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer handSprite;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        handSprite.flipY = mousePosition.x < player.position.x;
    }
}
