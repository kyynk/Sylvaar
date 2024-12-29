using Core;
using UnityEngine;

namespace Weapons
{
    public class Sword : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Sword";
        public WeaponType WeaponType => WeaponType.Sword;
        public float Damage => 50f;
        public float Range => 2f;
        public float CooldownTime => 1f;
        public int MaxDurability => 100; // 最大耐久度
        private int durability;

        private float lastAttackTime;

        private bool isAttacking = false;

        private void Start()
        {
            durability = MaxDurability; // 初始化耐久度
        }

        public void Attack()
        {
            if (CanAttack())
            {
                Debug.Log($"Sweap {WeaponName} attack!");
                lastAttackTime = Time.time;
                isAttacking = true;

                Collider weaponCollider = GetComponent<Collider>();
                if (weaponCollider != null)
                {
                    weaponCollider.enabled = true;
                    // Force update of physics collision
                    Physics.SyncTransforms();
                }
                
                Invoke(nameof(StopAttack), 0.2f);
            }
            else
            {
                Debug.Log($"{WeaponName} is cooling down.");
            }
        }

        private void StopAttack()
        {
            isAttacking = false;

            Collider weaponCollider = GetComponent<Collider>();
            if (weaponCollider != null)
            {
                weaponCollider.enabled = false;
            }
        }

        public bool CanAttack()
        {
            return Time.time >= lastAttackTime + CooldownTime;
        }

        public void ResetWeapon()
        {
            lastAttackTime = 0;
            durability = MaxDurability;
            Debug.Log($"{WeaponName} is reset.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isAttacking) return;

            if (other.tag == "Weapon" && other.name == "Shield")
            {
                Debug.Log($"Sword has been blocked");
                if (other.TryGetComponent<IDamageable>(out var shield))
                {
                    shield.TakeDamage(10);
                }
                ReduceDurability(10);
            }
            else if (other.tag == "Enemy")
            {
                Debug.Log($"Sword hits {other.name}");
                if (other.TryGetComponent<IDamageable>(out var damageable))
                {
                    Debug.Log($"Dealing {Damage} damage to {other.name}");
                    damageable.TakeDamage(Damage);
                }
            }
        }

        private void ReduceDurability(int amount)
        {
            durability -= amount;
            Debug.Log($"Sword durability reduced by {amount}. Remaining durability: {durability}");

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
