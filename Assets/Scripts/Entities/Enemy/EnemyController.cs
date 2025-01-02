using Core;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float changeDirectionInterval = 3f;
        [SerializeField] private float idleProbability = 0.2f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float detectRange = 10f;
        [SerializeField] private Transform playerTarget;

        private Enemy enemy;
        private Vector3 moveDirection;
        private float nextDirectionChangeTime;
        private Animator animator;
        private float playerDistance;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            playerDistance = GetPlayerDistance();
            if(_isPlayerInRange(playerDistance, attackRange))
            {
                animator.SetTrigger("FoxAttack");
                AttackPlayer();
            }
            else if (_isPlayerInRange(playerDistance, detectRange))
            {
                animator.ResetTrigger("FoxAttack");
                EnemyChasePlayer();
            }
            else
            {   
                animator.ResetTrigger("FoxAttack");
                HandleIdleOrRandomMove();
            }
        }

        public float GetPlayerDistance()
        {
            float playerDistance = Vector3.Distance(enemy.transform.position, playerTarget.position);
            return playerDistance;
        }

        public bool _isPlayerInRange(float distance, float range)
        {
            if(distance <= range){ return true;}
            else return false;
        }

        public void EnemyChasePlayer()
        {
            Vector3 targetPosition = playerTarget.position;
            targetPosition.y = enemy.transform.position.y;
            Vector3 moveDirection = (targetPosition - enemy.transform.position).normalized;

            MoveForward(moveDirection);
        }

        public void HandleIdleOrRandomMove()
        {  
            if (Time.time >= nextDirectionChangeTime)
            {
                if (Random.value < idleProbability)
                {
                    Debug.Log($"{gameObject.name} is idling.");
                    moveDirection = Vector3.zero;
                    nextDirectionChangeTime = Time.time + changeDirectionInterval;
                }
                else
                {
                    // random angle in degrees, converted to radians
                    float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                    moveDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;
                    nextDirectionChangeTime = Time.time + changeDirectionInterval;
                }
            }
            // Ensure movement occurs
            if (moveDirection != Vector3.zero)
            {
                MoveForward(moveDirection);
            }
        }

        public void MoveForward(Vector3 direction)
        {
            // move
            enemy.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            // adjust rotation
            if (direction != Vector3.zero)
            {
                Debug.Log($"{gameObject.name} move forward!");
                enemy.transform.forward = direction;
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
    }
}
