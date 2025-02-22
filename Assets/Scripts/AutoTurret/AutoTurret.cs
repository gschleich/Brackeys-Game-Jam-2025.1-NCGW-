using UnityEngine;
using System.Collections;

public class AutoTurret : MonoBehaviour
{
    public Transform target; // The current target enemy
    public float range = 5f; // Detection range of the turret
    public float fireRate = 1f; // Rate of fire (bullets per second)
    public GameObject TurretBulletPrefab; // Bullet prefab to shoot
    public Transform TurretBulletSpawnPoint; // Where bullets are fired from
    public SpriteRenderer spriteRenderer; // Assign the turret's sprite renderer
    private float fireCooldown = 0f; // Internal timer for shooting

    void Update()
    {
        // Fetch upgraded stats from the upgrade manager
        range = TurretUpgradeManager.Instance.GetUpgradedRange();
        fireRate = TurretUpgradeManager.Instance.GetUpgradedFireRate();

        FindTarget();

        if (target == null) return;

        Vector3 targetCenter = target.GetComponent<Collider2D>().bounds.center;
        Vector3 direction = targetCenter - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        bool shouldFlip = angle > 90 || angle < -90;
        spriteRenderer.flipY = shouldFlip;

        if (shouldFlip)
        {
            TurretBulletSpawnPoint.localPosition = new Vector3(
                TurretBulletSpawnPoint.localPosition.x,
                -Mathf.Abs(TurretBulletSpawnPoint.localPosition.y),
                TurretBulletSpawnPoint.localPosition.z
            );
        }
        else
        {
            TurretBulletSpawnPoint.localPosition = new Vector3(
                TurretBulletSpawnPoint.localPosition.x,
                Mathf.Abs(TurretBulletSpawnPoint.localPosition.y),
                TurretBulletSpawnPoint.localPosition.z
            );
        }

        TurretBulletSpawnPoint.rotation = Quaternion.identity;

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Find all enemies
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        target = nearestEnemy; // Assign nearest enemy as the target
    }

    void Shoot()
    {
        if (TurretBulletPrefab != null && TurretBulletSpawnPoint != null && target != null)
        {
            // Use the center of the target's collider for direction
            Vector3 targetCenter = target.GetComponent<Collider2D>().bounds.center;
            GameObject bullet = Instantiate(TurretBulletPrefab, TurretBulletSpawnPoint.position, TurretBulletSpawnPoint.rotation);
            TurretBulletController bulletScript = bullet.GetComponent<TurretBulletController>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection((targetCenter - TurretBulletSpawnPoint.position).normalized);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection range in Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
