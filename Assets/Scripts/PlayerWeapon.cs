using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject equippedWeapon; 
    public Transform weaponHolder; 

    public void EquipWeapon(GameObject newWeapon, float newFireRate)
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
        }

        equippedWeapon = Instantiate(newWeapon, weaponHolder.position, Quaternion.identity, weaponHolder);
        
        // Get the WeaponController component and update the fire rate
        WeaponController weaponController = equippedWeapon.GetComponent<WeaponController>();
        if (weaponController != null)
        {
            weaponController.SetFireRate(newFireRate);
        }

        Debug.Log("Equipped new weapon with fire rate: " + newFireRate);
    }
}

