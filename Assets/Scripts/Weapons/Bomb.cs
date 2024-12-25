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

        public float ThrowForce = 5f;
        public float ThrowAngle = 60f;
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

            // remove Bomb script，avoid reapet execute Attack
            Destroy(bombInstance.GetComponent<Bomb>());
            bombInstance.transform.localScale = new Vector3(1, 1, 1);

            // add Rigidbody compoment and Collider
            Rigidbody rb = bombInstance.AddComponent<Rigidbody>();
            SphereCollider collider = bombInstance.AddComponent<SphereCollider>();

            // 取得角色的前方方向 (朝向方向)
            Vector3 direction = transform.forward;  // 假設角色朝向是 Z 軸正方向

            // 定義旋轉角度（以弧度為單位）
            float xRotation = Mathf.Deg2Rad * 45;  // 旋轉 45 度繞 X 軸
            float yRotation = Mathf.Deg2Rad * 30;  // 旋轉 30 度繞 Y 軸
            float zRotation = Mathf.Deg2Rad * 60;  // 旋轉 60 度繞 Z 軸

            // 創建分別繞 X, Y, Z 軸的旋轉四元數
            Quaternion rotX = Quaternion.AngleAxis(xRotation * Mathf.Rad2Deg, Vector3.right);
            Quaternion rotY = Quaternion.AngleAxis(yRotation * Mathf.Rad2Deg, Vector3.up);
            Quaternion rotZ = Quaternion.AngleAxis(zRotation * Mathf.Rad2Deg, Vector3.forward);

            // 合併三個旋轉四元數
            Quaternion totalRotation = rotZ * rotY * rotX;  // 注意旋轉順序
            
            Vector3 throwDirection = (totalRotation * direction).normalized;

            // 設置拋出力量，並將其施加到剛體上
            rb.AddForce(throwDirection * ThrowForce, ForceMode.Impulse);

            // bomo explode after 5s
            Destroy(bombInstance, 5.1f);
            Invoke(nameof(Explode), 5f);
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
