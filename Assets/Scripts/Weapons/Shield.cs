using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// ¹ê²{¬ÞµPªºÅÞ¿è
    /// </summary>
    public class Shield : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Shield";
        public WeaponType WeaponType => WeaponType.Shield;
        public float Damage => 0f;
        public float Range => 0f;
        public float CooldownTime => 1f;

        private bool isBlocking = false;

        public void Attack()
        {
            Debug.Log("Shield cannot attack.");
        }

        public bool CanAttack() => false;

        public void ResetWeapon()
        {
            isBlocking = false;
            Debug.Log($"{WeaponName} is reset.");
        }

        public void Block()
        {
            isBlocking = true;
            Debug.Log($"{WeaponName} is blocking!");
        }

        public void StopBlock()
        {
            isBlocking = false;
            Debug.Log($"{WeaponName} stopped blocking.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isBlocking && other.tag == "Enemy")
            {
                Debug.Log($"{WeaponName} blocked an attack from {other.name}!");
            }
        }
    }
}
