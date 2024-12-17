using UnityEngine;

namespace Game.Weapons
{
    /// <summary>
    /// 實現近戰武器（如劍）的邏輯
    /// </summary>
    public class Sword : MonoBehaviour, IWeapon
    {
        public string WeaponName => "Sword"; // 武器名稱
        public float Damage => 50f;             // 傷害
        public float Range => 2f;               // 攻擊範圍
        public float CooldownTime => 1f;        // 攻擊冷卻時間

        private float lastAttackTime;           // 記錄最後一次攻擊的時間

        public void Attack()
        {
            if (CanAttack())
            {
                Debug.Log($"{WeaponName} attacks with {Damage} damage!");
                lastAttackTime = Time.time; // 更新最後攻擊時間
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
