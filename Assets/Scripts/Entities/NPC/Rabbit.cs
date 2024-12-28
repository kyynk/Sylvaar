using Interactable;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public class RabbitNPC : NPCInteractable
    {
        [SerializeField] private TriggerZoneWithInteract triggerZoneWithInteract;

        private void Start()
        {
            StoryManager.Instance.RegisterNPC("Rabbit");
        }

        public override void Interact()
        {
            triggerZoneWithInteract.TriggerAVG();
        }
    }
}

