using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float changeDirectionInterval = 3f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private Transform playerTarget;

        private Enemy enemy;
        private Vector3 moveDirection;
        private float nextDirectionChangeTime;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            if (!enemy.IsAlive) return;

            //MoveRandomly();

            if (playerTarget != null && Vector3.Distance(transform.position, playerTarget.position) <= attackRange)
            {
                AttackPlayer();
            }
        }

        private void MoveRandomly()
        {
            if (Time.time >= nextDirectionChangeTime)
            {
                // random angle in degrees
                float randomAngle = Random.Range(0f, 360f);
                moveDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
                nextDirectionChangeTime = Time.time + changeDirectionInterval;
            }

            // move
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // adjust rotation
            if (moveDirection != Vector3.zero)
            {
                transform.forward = moveDirection;
            }
        }

        private void AttackPlayer()
        {
            Debug.Log($"{gameObject.name} attacks the player!");

            // Example: Apply damage to the player
            if (playerTarget.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(enemy.Damage);
            }
        }

        public void SetTarget(Transform target)
        {
            playerTarget = target;
        }
    }
}
