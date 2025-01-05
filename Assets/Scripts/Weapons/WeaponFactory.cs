using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponFactory : MonoBehaviour
    {
        [SerializeField] private List<GameObject> weaponPrefabs;

        public GameObject CreateWeapon(string weaponName)
        {
            // find prefab by name
            GameObject weaponPrefab = weaponPrefabs.Find(w => w.name == weaponName);

            if (weaponPrefab == null)
            {
                //Debug.LogError($"Weapon '{weaponName}' not found in the factory!");
                return null;
            }

            // Instantiate the weapon
            GameObject weaponInstance = Instantiate(weaponPrefab);

            // set the pos on the player's hand, the weapon's pivot should be at the bottom
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;

            return weaponInstance;
        }

    }
}
