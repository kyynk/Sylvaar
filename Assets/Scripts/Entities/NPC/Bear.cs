using Core;
using Interactable;
using UnityEngine;

namespace Entities.NPC
{
    public class Bear : NPCInteractable
    {
        [SerializeField] private TriggerZoneWithInteract triggerZoneWithInteract;

        private void Start()
        {
            StoryManager.Instance.RegisterNPC("Bear");
        }

        public override void Interact()
        {
            triggerZoneWithInteract.TriggerAVG();
            StoryManager.Instance.TriggerQuest("Bear");
        }
    }
}
