using NPC;
using UnityEngine;

namespace KeyboardInput
{
    public class PlayerInteract : InputManager
    {
        protected override void CalculateDpadAxis()
        {
            
        }

        protected override void CalculateJump()
        {
            
        }

        protected override void CalculateRun()
        {
            
        }

        protected override void CalculateDialogClick()
        {
            
        }

        protected override void PostProcessDpadAxis()
        {
            
        }

        protected override void CalculateInteract()
        {
            if (Input.GetKeyDown("f"))
            {
                float interactRange = 1.5f;
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
                foreach (Collider collider in colliderArray)
                {
                    if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                    {
                        npcInteractable.Interact();
                    }
                }
            }
        }
    }
}