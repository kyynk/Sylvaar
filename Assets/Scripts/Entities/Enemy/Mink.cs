using Core;
using UnityEngine;
using System.Collections;

namespace Entities.Enemy
{
    public class Mink : MonoBehaviour, IDamageable
    {
        public float Health { get; private set; } = 50f;
        public float MaxHealth { get; private set; } = 50f;
        public float Damage { get; private set; } = 10f;
        public float MoveSpeed { get; private set; } = 3f;
        public bool IsAlive => Health > 0;
        [SerializeField] HealthBar healthBar;

        private void Awake() 
        {
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;
            Health -= damage;
            Health = Mathf.Clamp(Health, 0, MaxHealth);
            healthBar.UpdateHealthBar(Health,MaxHealth);
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            Health += amount;
            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }

        public void Die()
        {
            //Debug.Log($"{gameObject.name} has died!");
            Destroy(gameObject);
        }
        
        public void SetMoveSpeed(float newSpeed)
        {
            if (newSpeed > 0)
            {
                MoveSpeed = newSpeed;
            }
        }
    }
}