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
            dialogClick = Input.GetKeyDown("mouse 0");
            evtDialogClick?.Invoke(dialogClick);
        }

        protected override void PostProcessDpadAxis()
        {
        
        }

        protected override void CalculateInteract()
        {
            if (Input.GetKeyDown("f"))
            {
                IInteractable interactable = playerInteract.GetInteractableObject();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }

        // wait AVG
        // protected override void MuteCharacterMove(bool _isMute)
        // {
        //     isMute = _isMute;
        // }



    }
}
