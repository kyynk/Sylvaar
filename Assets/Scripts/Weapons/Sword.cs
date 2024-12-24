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

        private float lastAttackTime;

        private bool isAttacking = false;

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
