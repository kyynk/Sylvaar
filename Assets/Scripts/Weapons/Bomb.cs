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
            // 實例化炸彈
            GameObject bombInstance = Instantiate(gameObject, transform.position, Quaternion.identity);

            // 移除 Bomb 腳本，避免重複執行 Attack 等邏輯
            Destroy(bombInstance.GetComponent<Bomb>());
            bombInstance.transform.localScale = new Vector3(1, 1, 1);

            // 添加 Rigidbody 組件
            Rigidbody rb = bombInstance.AddComponent<Rigidbody>();
            // rb.isKinematic = false;

            SphereCollider collider = bombInstance.AddComponent<SphereCollider>();
            // collider.radius = 0.5f;

            // 獲取相機的正前方向
            Vector3 throwDirection = Camera.main.transform.forward;

            // 添加向上的分量，讓炸彈拋出時有拋物線效果
            throwDirection += Vector3.up * 4.5f;

            // 設置剛體的速度（模擬投擲）
            rb.AddForce(throwDirection.normalized * ThrowForce, ForceMode.Impulse);

            // 設定炸彈在 1 秒後爆炸
            Destroy(bombInstance, 5.1f);
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
