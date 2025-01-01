using Core;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float changeDirectionInterval = 3f;
        [SerializeField] private float idleProbability = 0.2f; // 停留的機率
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float detectRange = 10f;
        [SerializeField] private Transform playerTarget;

        private Enemy enemy;
        private Vector3 moveDirection;
        private float nextDirectionChangeTime;
        private Animator foxAnimator;
        private string currentAnimation = "";

        private void Awake()
        {
            foxAnimator = GetComponent<Animator>();
            enemy = GetComponent<Enemy>();
            SetTarget();
        }

        private void Update()
        {
            if (!enemy.IsAlive) return;

            float playerDistance = Vector3.Distance(transform.position, playerTarget.position);
            if (playerTarget != null && playerDistance <= detectRange)
            {
                if(playerDistance <= attackRange)
                {
                    ChangeAnimation("FoxAttackAnimation");
                    AttackPlayer();
                }
                else
                {
                    ChangeAnimation("FoxWalk");
                    moveDirection = (playerTarget.position - transform.position).normalized;
                    MoveForward(moveDirection);
                }
            }
            else
            {
                HandleIdleOrRandomMove();
            }
            CheckAnimation();
        }

        private void HandleIdleOrRandomMove()
        {  
            if (Time.time >= nextDirectionChangeTime)
            {
                if (Random.value < idleProbability)
                {
                    ChangeAnimation("FoxIdle");
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

                    ChangeAnimation("FoxWalk");
                    Debug.Log($"{gameObject.name} now is walking in direction: {moveDirection}!");
                }
            }

            // Ensure movement occurs
            if (moveDirection != Vector3.zero)
            {
                MoveForward(moveDirection);
            }
        }

        private void MoveForward(Vector3 direction)
        {
            // move
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // adjust rotation
            if (direction != Vector3.zero)
            {
                Debug.Log($"{gameObject.name} move forward!");
                transform.forward = direction;
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

        public void SetTarget()
        {
            if (playerTarget == null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    playerTarget = player.transform;
                }
            }
        }

        private void CheckAnimation()
        {
            if(currentAnimation == "FoxAttackAnimation") return;
            Debug.Log($"{gameObject.name} nothing to do!");
            ChangeAnimation("FoxIdle");
        }

        public void ChangeAnimation(string animation, float crossfade = 0.2f)
        {
            Debug.Log($"Change Animation {animation}!");
            if(currentAnimation != animation)
            {
                currentAnimation = animation;
                foxAnimator.CrossFade(animation, crossfade);
            }
        }
    }
}
