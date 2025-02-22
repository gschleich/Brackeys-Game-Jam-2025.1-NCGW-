using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; // Assign this in Inspector
    public float weaponFireRate = 0.3f; // Unique fire rate for this weapon

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the Player has the "Player" tag
        {
            PlayerWeapon playerWeapon = other.GetComponent<PlayerWeapon>();
            if (playerWeapon != null)
            {
                playerWeapon.EquipWeapon(weaponPrefab, weaponFireRate); // Pass the new fire rate
                Destroy(gameObject); // Remove pickup after collection
            }
        }
    }
}

