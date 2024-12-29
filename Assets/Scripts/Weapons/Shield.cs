using UnityEngine;
using Core;

namespace Weapons
{
    public class Shield : MonoBehaviour, IWeapon, IDamageable
    {
        public string WeaponName => "Shield";
        public WeaponType WeaponType => WeaponType.Shield;
        public float Damage => 0f;
        public float Range => 0f;
        public float CooldownTime => 1f;
        public int MaxDurability => 100; // 最大耐久度
        private int durability;

        private bool isBlocking = false;

        private void Start()
        {
            durability = MaxDurability;
        }

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

        public void TakeDamage(float damage)
        {
            durability -= Mathf.RoundToInt(damage); // 扣除耐久度
            Debug.Log($"{WeaponName} durability reduced by {damage}. Remaining durability: {durability}");

            if (durability <= 0)
            {
                DestroyWeapon();
            }
        }

        private void DestroyWeapon()
        {
            Debug.Log($"{WeaponName} has been destroyed due to zero durability.");
            Destroy(gameObject);
        }
    }
}
