using Core;
using Interactable;
using UnityEngine;

namespace Entities.NPC
{
    public class Monkey : NPCInteractable
    {
        [SerializeField] private TriggerZoneWithInteract triggerZoneWithInteract;

        private void Start()
        {
            StoryManager.Instance.RegisterNPC("Monkey");
        }

        public override void Interact()
        {
            triggerZoneWithInteract.TriggerAVG();
            StoryManager.Instance.TriggerQuest("Monkey");
        }
    }
}
