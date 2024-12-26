using UnityEngine;

namespace Interactable
{
    public class NPCInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactText;

        public virtual void Interact()
        {
            Debug.Log("NPC interact");
        }

        public string GetInteractText()
        {
            return interactText;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}