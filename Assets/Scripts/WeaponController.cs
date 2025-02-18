using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer weaponSprite;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        bool isFlipped = mousePosition.x < player.position.x;
        weaponSprite.flipY = isFlipped;

        if (isFlipped)
        {
            bulletSpawnPoint.localPosition = new Vector3(bulletSpawnPoint.localPosition.x, -Mathf.Abs(bulletSpawnPoint.localPosition.y), bulletSpawnPoint.localPosition.z);
        }
        else
        {
            bulletSpawnPoint.localPosition = new Vector3(bulletSpawnPoint.localPosition.x, Mathf.Abs(bulletSpawnPoint.localPosition.y), bulletSpawnPoint.localPosition.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
