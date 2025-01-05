using UnityEngine;
using Core;

namespace Weapons
{
    public class Shield : MonoBehaviour, IWeapon, IDamageable
    {
        public string WeaponName => "Shield";
        public WeaponType WeaponType => WeaponType.Shield;
        public float Damage => 0f;
        public float Range => 0f;
        public float CooldownTime => 1f;
        public int MaxDurability => 100;
        private int durability;

        private bool isBlocking = false;

        private void Start()
        {
            durability = MaxDurability;
        }
        private float lastBlockTime; 

        public void Attack()
        {
            //Debug.Log("Shield cannot attack.");
        }

        public bool CanAttack() => false;

        public bool CanBlock()
        {
            return Time.time >= lastBlockTime + CooldownTime;
        }

        public void ResetWeapon()
        {
            isBlocking = false;
            //Debug.Log($"{WeaponName} is reset.");
        }

        public void Block()
        {
            if (CanBlock())
            {
                isBlocking = true;
                lastBlockTime = Time.time;
                //Debug.Log($"{WeaponName} is blocking!");
            }
            else
            {
                //Debug.Log($"{WeaponName} is on cooldown. Wait {CooldownTime - (Time.time - lastBlockTime):F2} seconds.");
            }
        }

        public void StopBlock()
        {
            if (isBlocking)
            {
                isBlocking = false;
                //Debug.Log($"{WeaponName} stopped blocking.");
            }
        }

        public void TakeDamage(float damage)
        {
            durability -= Mathf.RoundToInt(damage);
            //Debug.Log($"{WeaponName} durability reduced by {damage}. Remaining durability: {durability}");

            if (durability <= 0)
            {
                PlayerBag playerBag = GameObject.Find("PlayerBag").GetComponent<PlayerBag>();
                DestroyWeapon();
                playerBag.RemoveItem(WeaponName);
            }
        }

        private void DestroyWeapon()
        {
            //Debug.Log($"{WeaponName} has been destroyed due to zero durability.");
            Destroy(gameObject);
        }

        //private void Update()
        //{
        //    if (isBlocking)
        //    {
        //        // limit other actions while blocking, like slow moving ...
        //        //Debug.Log($"{WeaponName} is actively blocking for {Time.time - blockStartTime:F2} seconds.");
        //    }
        //}
    }
}
