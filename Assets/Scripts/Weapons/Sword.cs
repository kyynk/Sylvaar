using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// 實現近戰武器（如劍）的邏輯
    /// </summary>
    public class Sword : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Sword"; // 武器名稱
        public float Damage => 50f;         // 傷害
        public float Range => 2f;           // 攻擊範圍
        public float CooldownTime => 1f;    // 攻擊冷卻時間

        private float lastAttackTime;       // 記錄最後一次攻擊的時間

        private bool isAttacking = false;   // 用於標記當前是否處於攻擊中

        public void Attack()
        {
            if (CanAttack())
            {
                Debug.Log($"{WeaponName} attacks with {Damage} damage!");
                lastAttackTime = Time.time;
                isAttacking = true;

                Collider weaponCollider = GetComponent<Collider>();
                if (weaponCollider != null)
                {
                    weaponCollider.enabled = true;
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
            Debug.Log($"{WeaponName} is reset.");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Sword Collision detected");
            if (!isAttacking) return;

            if (other.tag == "Enemy")
            {
                Debug.Log($"Sword hits {other.name}");
                if (other.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(Damage);
                }
            }
        }
    }
}
