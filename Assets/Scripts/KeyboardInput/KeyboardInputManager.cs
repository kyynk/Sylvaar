using Interactable;
using UnityEngine;

namespace KeyboardInput
{
    public class KeyboardInputManager : InputManager
    {
        [SerializeField] private PlayerInteract playerInteract;
        private Vector3 axis;
        private bool jump;
        private bool run;
        private bool dialogClick;
        private bool isAttacking = false;
        private bool isInDialog;
        private bool attack;
        // wait AVG
        // private bool isMute;

        protected override void CalculateDpadAxis()
        {
            axis = Vector3.zero;
            if (Input.GetKey("w"))
            {
                axis.z = 1.0f;
            }
            if (Input.GetKey("s"))
            {
                axis.z = -1.0f;
            }
            if (Input.GetKey("d"))
            {
                axis.x = 1.0f;
            }
            if (Input.GetKey("a"))
            {
                axis.x = -1.0f;
            }
            evtDpadAxis?.Invoke(axis);
            // wait AVG
            // evtDpadAxis?.Invoke(isMute ? Vector2.zero : axis);
        }

        protected override void CalculateJump()
        {
            jump = Input.GetKeyDown("space");
            evtJump?.Invoke(jump);
            // wait AVG
            // evtJump?.Invoke(isMute ? false: jump);
        }

        protected override void CalculateRun()
        {
            run = Input.GetKey("right shift");
            evtRun?.Invoke(run);
            // wait AVG
            // evtRun?.Invoke(isMute ? false: run);
        }

        protected override void CalculateDialogClick()
        {
            if (isInDialog)
            {
                dialogClick = UnityEngine.Input.GetKeyDown("mouse 0");
                evtDialogClick?.Invoke(dialogClick);
            }
        }

        protected override void CalculateAttack()
        {
            if (!isInDialog)
            {
                attack = UnityEngine.Input.GetKeyDown("mouse 0");
                evtAttack?.Invoke(attack);
            }
        }

        protected override void PostProcessDpadAxis()
        {
        
        }

        public void EnterDialogMode()
        {
            isInDialog = true;
        }

        public void ExitDialogMode()
        {
            isInDialog = false;
        }

        // wait AVG
        // protected override void MuteCharacterMove(bool _isMute)
        // {
        //     isMute = _isMute;
        // }



    }
}
