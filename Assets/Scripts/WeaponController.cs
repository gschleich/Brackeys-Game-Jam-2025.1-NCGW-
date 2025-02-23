using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer weaponSprite;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f; // Default fire rate (seconds per shot)

    private float nextFireTime = 0f; 

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

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Left mouse button + Fire rate check
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set the next fire time
        }
    }

    void Shoot()
    {
        SoundManager.Instance.PlaySound2D("Shoot");
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    public void SetFireRate(float newFireRate)
    {
        fireRate = newFireRate;
        Debug.Log("Fire rate set to: " + fireRate);
    }
}
