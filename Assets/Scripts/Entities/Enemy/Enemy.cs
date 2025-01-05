using Core;
using UnityEngine;
using System.Collections;

namespace Entities.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public float Health { get; private set; } = 200f;
        public float MaxHealth { get; private set; } = 200f;
        public float Damage { get; private set; } = 10f;
        public float MoveSpeed { get; private set; } = 10f;
        public bool IsAlive => Health > 0;

        [SerializeField] HealthBar healthBar;
        private Animator animator;

        private void Awake() 
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;
            //Debug.Log("Enemy takes damage!"); 
            //Debug.Log("Enemy takes damage!"); 
            animator.SetTrigger("FoxIsAttacked");
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
            animator.SetTrigger("FoxDie");
            StartCoroutine(WaitForAnimationAndDestroy());
        }

        private IEnumerator WaitForAnimationAndDestroy()
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.ResetTrigger("FoxDie");
            Destroy(gameObject);
        }
    }
}
