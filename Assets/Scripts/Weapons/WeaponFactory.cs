using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] private Transform playerWeaponHandle; //the pos that the weapon will be attached to
    [SerializeField] private List<GameObject> weaponPrefabs;

    public GameObject CreateWeapon(string weaponName)
    {
        // find prefab by name
        GameObject weaponPrefab = weaponPrefabs.Find(w => w.name == weaponName);

        if (weaponPrefab == null)
        {
            Debug.LogError($"Weapon '{weaponName}' not found in the factory!");
            return null;
        }

        // Instantiate the weapon
        GameObject weaponInstance = Instantiate(weaponPrefab, playerWeaponHandle);

        // set the pos on the player's hand, the weapon's pivot should be at the bottom
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localPosition = new Vector3(3f, 1.5f, 0);
        weaponInstance.transform.localRotation = Quaternion.identity;

        return weaponInstance;
    }

    public void DestroyCurrentWeapon()
    {
        if (playerWeaponHandle.childCount > 0)
        {
            foreach (Transform child in playerWeaponHandle)
            {
                // check if child name in weaponPrefabs
                if (weaponPrefabs.Exists(w => (w.name + "(Clone)") == child.name ))
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public GameObject EquipWeapon(string weaponName)
    {
        //DestroyCurrentWeapon();

        return CreateWeapon(weaponName);
    }
}
