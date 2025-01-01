using Core;
using UnityEngine;

namespace Entities.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public float Health { get; private set; } = 50f;
        public float MaxHealth { get; private set; } = 100f;
        public float Damage { get; private set; } = 10f;
        public float MoveSpeed { get; private set; } = 10f;
        public bool IsAlive => Health > 0;

        [SerializeField] HealthBar healthBar;

        // private Rigidbody rb;

        private void Awake() 
        {
            // rb = GetComponentInChildren<Rigidbody>();
            healthBar = GetComponentInChildren<HealthBar>();
        }

        private void Start() 
        {
            Health = MaxHealth;
            healthBar.UpdateHealthBar(Health,MaxHealth);
            // target = GameObject.Find("Player").transform;
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;
            Debug.Log("Enemy takes damage!"); 
            Health -= damage;
            Health = Mathf.Clamp(Health, 0, MaxHealth);
            healthBar.UpdateHealthBar(Health,MaxHealth);

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
            Destroy(gameObject);
        }
    }
}
