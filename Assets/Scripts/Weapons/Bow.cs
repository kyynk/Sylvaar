using UnityEngine;

namespace Game.Weapons
{
    /// <summary>
    /// 實現遠程武器（如弓箭）的邏輯
    /// </summary>
    public class Bow : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Bow"; // 武器名稱
        public float Damage => 30f;                 // 傷害
        public float Range => 15f;                  // 射程
        public float CooldownTime => 0.5f;          // 攻擊冷卻時間

        private float lastAttackTime;

        public void Attack()
        {
            if (CanAttack())
            {
                Debug.Log($"{WeaponName} fires an arrow with {Damage} damage!");
                lastAttackTime = Time.time;
                // 這裡可以添加箭的生成邏輯，例如 Instantiate 一個箭的 Prefab
            }
            else
            {
                Debug.Log($"{WeaponName} is cooling down.");
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
