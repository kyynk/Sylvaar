using AVG;
using UnityEngine;
using UnityEngine.Events;

namespace KeyboardInput
{
    public abstract class InputManager : MonoBehaviour
    {
        public UnityEvent<Vector3> evtDpadAxis;
        public UnityEvent<bool> evtJump;
        public UnityEvent<bool> evtRun;
        public UnityEvent<bool> evtDialogClick;
        public UnityEvent<bool> evtAttack;
    
        protected abstract void CalculateDpadAxis();
        protected abstract void CalculateJump();
        protected abstract void CalculateRun();
        protected abstract void CalculateDialogClick();
        protected abstract void CalculateAttack();
        protected abstract void PostProcessDpadAxis();
        protected abstract void CalculateInteract();
        // this func for trigger AVG dialog
        protected abstract void MuteCharacterMove(bool _isMute);

        private void Update()
        {
            // wait for AVG
            MuteCharacterMove(!AVGMachine.Instance.IsFinished());
            CalculateInteract();
            CalculateDpadAxis();
            CalculateJump();
            CalculateRun();
            CalculateDialogClick();
            CalculateAttack();
            PostProcessDpadAxis();
        }
    }
}
