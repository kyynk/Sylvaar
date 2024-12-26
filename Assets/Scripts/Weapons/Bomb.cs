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
            // create bomb
            GameObject bombInstance = Instantiate(gameObject, transform.position, Quaternion.identity);

            // remove Bomb script，avoid reapet execute Attack
            Destroy(bombInstance.GetComponent<Bomb>());
            bombInstance.transform.localScale = new Vector3(1, 1, 1);

            // add Rigidbody compoment and Collider
            Rigidbody rb = bombInstance.AddComponent<Rigidbody>();
            SphereCollider collider = bombInstance.AddComponent<SphereCollider>();

            //Setup throw direstion to main camera
            Vector3 throwDirection = Camera.main.transform.forward;

            rb.AddForce(throwDirection.normalized * 15f, ForceMode.Impulse);
            // rb.velocity = throwDirection * 50f;

            // bomo explode after 5s
            Destroy(bombInstance, 5.1f);
            Invoke(nameof(Explode), 5f);

            // destory handheld bomb 
            Destroy(gameObject);
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
