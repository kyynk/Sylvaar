using Core;
using Interactable;
using UnityEngine;

namespace Entities.NPC
{
    public class Raccoon : NPCInteractable
    {
        [SerializeField] private TriggerZoneWithInteract triggerZoneWithInteract;

        private void Start()
        {
            StoryManager.Instance.RegisterNPC("Raccoon");
        }

        public override void Interact()
        {
            triggerZoneWithInteract.TriggerAVG();
            StoryManager.Instance.TriggerQuest("Raccoon");
        }
    }
}

