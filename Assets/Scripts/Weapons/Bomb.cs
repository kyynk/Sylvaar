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

        public float ThrowForce = 10f;
        public float ThrowAngle = 45f;
        public float AfterThrowDestroyTime = 2.1f;

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
            // create bomb
            GameObject bombInstance = Instantiate(gameObject, transform.position, Quaternion.identity);

            // delete coiped bomb script, avoid reapet excute Attack
            Destroy(bombInstance.GetComponent<Bomb>());

            // add rigibody by bomb
            Rigidbody bombRigidbody = bombInstance.AddComponent<Rigidbody>();

            // calculate throw direction
            Vector3 throwDirection = CalculateThrowDirection();
            bombRigidbody.AddForce(throwDirection * ThrowForce, ForceMode.VelocityChange);

            // past 2 second Bomb explode
            Destroy(bombInstance, AfterThrowDestroyTime); // add a little time, make sure have been deleted after explode
            Invoke(nameof(Explode), 1f);
        }

        private Vector3 CalculateThrowDirection()
        {
            // according current forwad and angle to calculate throw direction
            Vector3 forward = transform.forward;
            Vector3 upward = Vector3.up * Mathf.Tan(ThrowAngle * Mathf.Deg2Rad);

            return (forward + upward).normalized;
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
