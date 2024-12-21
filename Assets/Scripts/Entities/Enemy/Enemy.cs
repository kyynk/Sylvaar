using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float Health { get; private set; } = 50f;
        public float MaxHealth { get; private set; } = 50f;
        public float Damage { get; private set; } = 10f;
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
            Debug.Log($"{gameObject.name} has died!");
            // Optional: Disable or destroy the enemy object
            Destroy(gameObject);
        }
    }
}
