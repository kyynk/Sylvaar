using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class NPCInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected string npcID;
        [SerializeField] protected List<string> interactText;
        public int dialogIndex { get; set; }

        public virtual void Interact()
        {
            Debug.Log("NPC interact");
        }

        public string GetInteractText()
        {
            return interactText[dialogIndex];
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}