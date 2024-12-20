using AVG;
using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    public abstract class InputManager : MonoBehaviour
    {
        public UnityEvent<Vector3> evtDpadAxis;
        public UnityEvent<bool> evtJump;
        public UnityEvent<bool> evtRun;
        public UnityEvent<bool> evtDialogClick;
    
        protected abstract void CalculateDpadAxis();
        protected abstract void CalculateJump();
        protected abstract void CalculateRun();
        protected abstract void CalculateDialogClick();
        protected abstract void PostProcessDpadAxis();
        // this func for trigger AVG dialog
        // protected abstract void MuteCharacterMove(bool _isMute);

        private void Update()
        {
            // wait for AVG
            // MuteCharacterMove(!AVGMachine.Instance.IsFinished());
            CalculateDpadAxis();
            CalculateJump();
            CalculateRun();
            CalculateDialogClick();
            PostProcessDpadAxis();
        }
    }
}
