using Core;
using Interactable;
using UnityEngine;

namespace Entities.NPC
{
    public class Monkey : NPCInteractable
    {
        [SerializeField] private TriggerZoneWithInteract triggerZoneWithInteract;
        [SerializeField] private int missionStoneCount;
        [SerializeField] private int missionWoodCount;
        [SerializeField] private QuestSystem npcQuestSystem;

        private void Start()
        {
            StoryManager.Instance.RegisterNPC("Monkey");
        }

        public override void Interact()
        {
            if (npcQuestSystem.GetScriptID("monkey") == 1)
            {
                if (IsMissionDone())
                {
                    npcQuestSystem.SetScriptsID("monkey", 2);
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
