using System.Collections.Generic;
using Core;
using UnityEngine;
using Weapons;

namespace Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        private enum Hands
        {
            Left,
            Right
        }
        public enum STATE
        {
            IDLE,
            LOCALMOTION,
            JUMP,
            FALL,
            ROLL,
            ATTACK
        }

        [SerializeField]
        private STATE state;

        public float velocity = 3.0f;
        public float jumpThrust = 3.0f;
        public GameObject model;

        private Vector3 movingVec;
        private Animator anim;
        private Rigidbody rigid;
        [SerializeField]
        private bool isThrust = false;
        [SerializeField]
        private bool isRunning = false;
        private bool triggerEnter = false;
        private List<GameObject> currentWeapon;
        [SerializeField] private Transform rightHand;  
        [SerializeField] private Transform leftHand;   

        private void Awake()
        {
            GameManager.Instance.LoadPlayerHandler(gameObject);
            anim = model.GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            currentWeapon = new List<GameObject> { null, null };
            state = STATE.IDLE;
        }

        private void Update()
        {
            Vector3 newVelocity = Vector3.zero;
            float movingVecH = Vector3.Dot(movingVec, transform.right);
            float movingVecV = Vector3.Dot(movingVec, transform.forward);
            AnimatorStateInfo stateInfo;

            if (Mathf.Abs(movingVecH) >= Mathf.Abs(movingVecV))
            {
                movingVec = movingVecH * transform.right;
            }
            else
            {
                movingVec = movingVecV * transform.forward;
            }

            switch (state)
            {
                case STATE.IDLE:
                    if (triggerEnter)
                    {
                        // wait animation
                        anim.CrossFadeInFixedTime("idle", 0.1f);
                        triggerEnter = false;
                        break;
                    }
                    if (movingVec.magnitude > 0.1f)
                    {
                        GoToState(STATE.LOCALMOTION);
                        break;
                    }
                    newVelocity = Vector3.zero;
                    break;
                case STATE.LOCALMOTION:
                    if (triggerEnter)
                    {
                        // wait animation
                        anim.CrossFadeInFixedTime("localmotion", 0.1f);
                        triggerEnter = false;
                        break;
                    }
                    if (movingVec.magnitude <= 0.1f)
                    {
                        GoToState(STATE.IDLE);
                        break;
                    }
                    // wait animation
                     anim.SetFloat("speed", isRunning ? 3.0f : 1.0f);
                    newVelocity = movingVec * (velocity * (isRunning ? 3.0f : 1.0f));
                    model.transform.forward = Vector3.Slerp(
                        model.transform.forward, movingVec, 0.1f);
                    break;
                case STATE.JUMP:
                    if (triggerEnter)
                    {
                        // wait animation
                        anim.CrossFadeInFixedTime("jump", 0.1f);
                        triggerEnter = false;
                        newVelocity = rigid.velocity;
                        break;
                    }
                    newVelocity = rigid.velocity;
                    stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                    // wait animation, then can use normalizedTime to check when to fall
                    // if (stateInfo.normalizedTime >= 0.8f && stateInfo.IsName("jump"))
                    if (stateInfo.IsName("jump"))
                    {
                        GoToState(STATE.FALL);
                    }
                    break;
                case STATE.FALL:
                    if (triggerEnter)
                    {
                        // wait animation
                        anim.CrossFadeInFixedTime("fall", 0.1f);
                        triggerEnter = false;
                        newVelocity = rigid.velocity;
                        break;
                    }
                    newVelocity = rigid.velocity;
                    break;
                case STATE.ROLL:
                    if (triggerEnter)
                    {
                        // wait animation
                        anim.CrossFadeInFixedTime("roll", 0.1f);
                        triggerEnter = false;
                        newVelocity = rigid.velocity;
                        break;
                    }
                    stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                    newVelocity = rigid.velocity;
                    // wait animation, then can use normalizedTime to check when to idle
                    // if (stateInfo.normalizedTime >= 0.8f && stateInfo.IsName("roll"))
                    if (stateInfo.IsName("roll"))
                    {
                        GoToState(STATE.IDLE);
                    }
                    break;
                case STATE.ATTACK:
                    stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                    bool checkAttack = stateInfo.IsName("Sword") || stateInfo.IsName("Bomb");
                    //allow move while attacking
                    //if (movingVec.magnitude > 0.1f)
                    //{
                    //    newVelocity = movingVec * (velocity * (isRunning ? 3.0f : 1.0f));
                    //    model.transform.forward = Vector3.Slerp(model.transform.forward, movingVec, 0.1f);
                    //}
                    //else
                    //{
                    //    newVelocity = Vector3.zero;
                    //}
                    if (checkAttack && stateInfo.normalizedTime >= 1f)
                    {
                        GoToState(STATE.IDLE);
                    }
                    break;
                default:
                    break;
            }

            // transform.Translate(movingVec * (velocity * Time.deltaTime), Space.World);
            // actually need to move to fixed update
            // rigid.velocity = movingVec * velocity;

            newVelocity.y = rigid.velocity.y + (isThrust ? 1.0f : 0.0f) * jumpThrust;
            rigid.velocity = newVelocity;
            isThrust = false;
        }

        private void GoToState(STATE targetState)
        {
            state = targetState;
            triggerEnter = true;
        }

        public void Move(Vector3 vector)
        {
            movingVec = vector;
        }

        public void Jump(bool _isThrust)
        {
            if (_isThrust)
            {
                if (state == STATE.IDLE || state == STATE.LOCALMOTION)
                {
                    isThrust = true;
                    GoToState(STATE.JUMP);
                }
            }
        }

        public void Run(bool _isRunning)
        {
            isRunning = _isRunning;
        }

        // public void OnCollisionEnter(Collision collision)
        // {
        //     if (state == STATE.JUMP)
        //     {
        //         GoToState(STATE.IDLE);
        //     }
        //     else if (state == STATE.FALL)
        //     {
        //         GoToState(STATE.ROLL);
        //     }
        // }

        public void IsGround(bool _isGround)
        {
            if (_isGround)
            {
                if (state == STATE.JUMP)
                {
                    AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                    // wait animation, then can use normalizedTime to check when to idle
                    // if (stateInfo.normalizedTime > 0.7f && stateInfo.IsName("jump"))
                    if (stateInfo.IsName("jump"))
                    {
                        GoToState(STATE.IDLE);
                    }
                }
                else if (state == STATE.FALL)
                {
                    GoToState(STATE.ROLL);
                }
            }
            else
            {
                if (state == STATE.IDLE || state == STATE.LOCALMOTION)
                {
                    GoToState(STATE.FALL);
                }
            }
        }

        public void EquipWeapon(GameObject weaponGameObj)
        {
            IWeapon weapon = weaponGameObj.GetComponent<IWeapon>();
            if (weapon.WeaponType != WeaponType.Shield) //left
            {
                DestroyLeftHandWeapon();
                currentWeapon[(int)Hands.Left] = weaponGameObj;
                currentWeapon[(int)Hands.Left].transform.SetParent(rightHand);
                currentWeapon[(int)Hands.Left].transform.localPosition = Vector3.zero;
                currentWeapon[(int)Hands.Left].transform.localRotation = Quaternion.identity;
            }
            
            if(weapon.WeaponType == WeaponType.Shield) //right
            {
                DestroyRightHandWeapon();
                currentWeapon[(int)Hands.Right] = weaponGameObj;
                currentWeapon[(int)Hands.Right].transform.SetParent(leftHand);
                currentWeapon[(int)Hands.Right].transform.localPosition = Vector3.zero;
                currentWeapon[(int)Hands.Right].transform.localRotation = Quaternion.identity;
            }
        }

        public void DestroyLeftHandWeapon()
        {
            if(currentWeapon[(int)Hands.Left] != null)
                Destroy(currentWeapon[(int)Hands.Left]);
        }

        public void DestroyRightHandWeapon()
        {
            if (currentWeapon[(int)Hands.Right] != null)
                Destroy(currentWeapon[(int)Hands.Right]);
        }

        public void Attack(bool _isAttack)
        {
            if(_isAttack && currentWeapon[(int)Hands.Left] != null)
            {
                Debug.Log("PlayerController Attack");
                IWeapon weapon = currentWeapon[(int)Hands.Left].GetComponent<IWeapon>();
                if (state == STATE.ATTACK || currentWeapon == null)
                    return;

                if(currentWeapon[(int)Hands.Left] == null)
                {
                    return;
                }

                if (weapon.CanAttack())
                {
                    state = STATE.ATTACK;
                    anim.CrossFadeInFixedTime(weapon.WeaponName, 0.1f);
                    weapon.Attack();
                }
            }
        }

        public void Block(bool _isBlock)
        {
            if (_isBlock && currentWeapon[(int)Hands.Right] != null)
            {
                Debug.Log("PlayerController Block");
                Shield weapon = currentWeapon[(int)Hands.Right].GetComponent<Shield>();

                // ensure not attack，and have shield
                if (state == STATE.ATTACK || currentWeapon == null)
                    return;

                if (weapon.CanBlock())
                {
                    state = STATE.ATTACK;
                    currentWeapon[(int)Hands.Right].transform.localPosition = new Vector3(0, 0, 0.02f);
                    currentWeapon[(int)Hands.Right].transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    anim.CrossFadeInFixedTime(weapon.WeaponName, 0.1f);
                    weapon.Block();
                }
            }
            else if(!_isBlock && currentWeapon[(int)Hands.Right] != null)
            {
                if (state == STATE.ATTACK)
                {
                    state = STATE.IDLE;
                    Shield weapon = currentWeapon[(int)Hands.Right]?.GetComponent<Shield>();
                    currentWeapon[(int)Hands.Right].transform.localPosition = new Vector3(0, 0, 0);
                    currentWeapon[(int)Hands.Right].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    anim.CrossFadeInFixedTime("idle", 0.1f);
                    weapon?.StopBlock();
                }
            }
        }


    }
}