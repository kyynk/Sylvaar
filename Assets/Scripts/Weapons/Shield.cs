using UnityEngine;

namespace Weapons
{
    public class Shield : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Shield";
        public WeaponType WeaponType => WeaponType.Shield;
        public float Damage => 0f;
        public float Range => 0f;
        public float CooldownTime => 1f; 

        private bool isBlocking = false;
        private float lastBlockTime; 

        public void Attack()
        {
            Debug.Log("Shield cannot attack.");
        }

        public bool CanAttack() => false;

        public bool CanBlock()
        {
            return Time.time >= lastBlockTime + CooldownTime;
        }

        public void ResetWeapon()
        {
            isBlocking = false;
            Debug.Log($"{WeaponName} is reset.");
        }

        public void Block()
        {
            if (CanBlock())
            {
                isBlocking = true;
                lastBlockTime = Time.time;
                Debug.Log($"{WeaponName} is blocking!");
            }
            else
            {
                Debug.Log($"{WeaponName} is on cooldown. Wait {CooldownTime - (Time.time - lastBlockTime):F2} seconds.");
            }
        }

        public void StopBlock()
        {
            if (isBlocking)
            {
                isBlocking = false;
                Debug.Log($"{WeaponName} stopped blocking.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isBlocking && other.CompareTag("Enemy"))
            {
                Debug.Log($"{WeaponName} blocked an attack from {other.name}!");
            }
        }

        //private void Update()
        //{
        //    if (isBlocking)
        //    {
        //        // limit other actions while blocking, like slow moving ...
        //        Debug.Log($"{WeaponName} is actively blocking for {Time.time - blockStartTime:F2} seconds.");
        //    }
        //}

    }
}
