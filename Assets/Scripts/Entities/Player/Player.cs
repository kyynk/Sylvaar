using Core;
using UnityEngine;

namespace Entities.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        public float Health { get; private set; } = 100f;
        public float MaxHealth { get; private set; } = 100f;
        public bool IsAlive => Health > 0;

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;

            Health -= damage;
            Health = Mathf.Clamp(Health, 0, MaxHealth);

            if (!IsAlive)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            Health += amount;
            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }

        private void Die()
        {
            Debug.Log("Player has died!");
            // Handle death logic here
        }
    }
}
