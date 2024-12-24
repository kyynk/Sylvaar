using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Player
{
    public class CameraController : MonoBehaviour
    {
        [Header("Target Settings")]
        public Transform target;

        public Vector3 offset = new Vector3(0, 2, -5);

        [Header("Camera Movement")]
        public float smoothTime = 0.3f;

        public float rotationSpeed = 5f;
        public float lockOnRotationSpeed = 5f;
        public float autoLockDistance = 5f;

        [Header("Camera Collision")]
        public float minDistance = 1f;
        public LayerMask collisionLayers;
        public LayerMask enemyLayer;

        [Header("Lock On Settings")]
        public float lockOnFOV = 160f;
        public float lockOnDistance = 20f;

        private bool _isLockOn;
        private Transform _lockOnTarget;
        private float currentRotationX;
        private float currentRotationY;

        private Vector3 currentVelocity = Vector3.zero;
        private Vector2 lookDelta;

        public UnityEvent evtUnlock;
        public UnityEvent<GameObject> evtLock;

        private void Awake()
        {
            enemyLayer = LayerMask.GetMask("Enemy");
        }

        private void LateUpdate()
        {
            HandleInput();

            if (_lockOnTarget && Vector3.Distance(_lockOnTarget.position, target.position) > lockOnDistance)
            {
                _lockOnTarget = null;
                _isLockOn = false;
                evtUnlock.Invoke();
            }

            if (!_lockOnTarget)
                HandleCameraRotation();

            var desiredPosition = target.position +
                                  target.right * offset.x +
                                  Vector3.up * offset.y +
                                  -transform.forward * Mathf.Abs(offset.z);
            var finalPosition = CheckCameraCollision(desiredPosition);

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref currentVelocity, smoothTime);

            if (Vector3.Distance(transform.position, finalPosition) < autoLockDistance)
                transform.position = finalPosition;

            if (_lockOnTarget)
            {
                var lockOnPosition = _lockOnTarget.position + Vector3.up * offset.y;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((lockOnPosition - transform.position).normalized), lockOnRotationSpeed * Time.deltaTime);
            }
            else
            {
                var targetPosition = target.position + Vector3.up * offset.y;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((targetPosition - transform.position).normalized), rotationSpeed * Time.deltaTime);
            }
        }

        private void HandleInput()
        {
            // 獲取滑鼠移動輸入
            lookDelta.x = Input.GetAxis("Mouse X");
            lookDelta.y = Input.GetAxis("Mouse Y");

            // 檢查是否觸發 Lock On
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                LockOn();
            }
        }

        private void HandleCameraRotation()
        {
            currentRotationY += lookDelta.x * rotationSpeed;
            currentRotationX -= lookDelta.y * rotationSpeed;
            currentRotationX = Mathf.Clamp(currentRotationX, -40f, 40f);

            var targetRotation = Quaternion.Euler(currentRotationX, currentRotationY, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private Vector3 CheckCameraCollision(Vector3 desiredPosition)
        {
            return desiredPosition;
        }

        private void LockOn()
        {
            if (_isLockOn)
            {
                _lockOnTarget = null;
                _isLockOn = false;
                evtUnlock.Invoke();
                return;
            }

            var enemiesInRange = new Collider[10];
            var count = Physics.OverlapSphereNonAlloc(target.position, lockOnDistance, enemiesInRange, enemyLayer);
            if (count == 0) return;

            var closestEnemy = enemiesInRange
                .Take(count)
                .Select(c => c.transform)
                .Where(e => Vector3.Angle(target.forward, e.position - target.position) <= lockOnFOV)
                .OrderBy(e => Vector3.Distance(target.position, e.position))
                .FirstOrDefault();

            if (closestEnemy == null) return;

            _lockOnTarget = closestEnemy;
            _isLockOn = true;
            evtLock.Invoke(_lockOnTarget.gameObject);
        }
    }
}
