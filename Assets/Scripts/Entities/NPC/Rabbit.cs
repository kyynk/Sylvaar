using Core;
using Interactable;
using UnityEngine;

namespace Entities.NPC
{
    public class Rabbit : NPCInteractable
    {
        [SerializeField] private TriggerZoneWithInteract triggerZoneWithInteract;

        private void Start()
        {
            StoryManager.Instance.RegisterNPC("Rabbit");
        }

        public override void Interact()
        {
            triggerZoneWithInteract.TriggerAVG();
            StoryManager.Instance.TriggerQuest("Rabbit");
        }
    }
}

