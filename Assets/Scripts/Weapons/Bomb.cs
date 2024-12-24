using Core;
using UnityEngine;

namespace Weapons
{
    public class Bomb : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Bomb";
        public WeaponType WeaponType => WeaponType.Bomb;
        public float Damage => 100f;
        public float Range => 5f;
        public float CooldownTime => 2f;

        private float lastAttackTime;

        public void Attack()
        {
            if (CanAttack())
            {
                Debug.Log($"{WeaponName} is thrown!");
                lastAttackTime = Time.time;

                ThrowBomb();
            }
            else
            {
                Debug.Log($"{WeaponName} is cooling down.");
            }
        }

        private void ThrowBomb()
        {
            Invoke(nameof(Explode), 1f);
        }

        private void Explode()
        {
            Debug.Log($"{WeaponName} exploded, dealing {Damage} damage!");

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range);
            foreach (var hit in hitColliders)
            {
                if (hit.CompareTag("Enemy") && hit.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(Damage);
                }
            }

            Destroy(gameObject);
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
    }
}
