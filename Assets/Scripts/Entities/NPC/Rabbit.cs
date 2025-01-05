using Core;
using Interactable;
using UnityEngine;

namespace Entities.NPC
{
    public class Rabbit : NPCInteractable
    {
        [SerializeField] private TriggerZoneWithInteract triggerZoneWithInteract;
        [SerializeField] private int missionStoneCount;
        [SerializeField] private int missionWoodCount;
        [SerializeField] private QuestSystem npcQuestSystem;

        private void Start()
        {
            StoryManager.Instance.RegisterNPC("Rabbit");
        }

        public override void Interact()
        {
            if (npcQuestSystem.GetScriptID("rabbit") == 1)
            {
                if (IsMissionDone())
                {
                    npcQuestSystem.SetScriptsID("rabbit", 2);
                    DeleteItem();
                }
                triggerZoneWithInteract.TriggerAVG();
            }
            else
            {
                triggerZoneWithInteract.TriggerAVG();
            }
        }

        private bool IsMissionDone()
        {
            // Get Item And Check Count
            return true;
        }

        private void DeleteItem()
        {
            // Delete
        }
    }
}

