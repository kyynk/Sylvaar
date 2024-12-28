using Interactable;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public class BearNPC : NPCInteractable
    {

        private void Start()
        {
            npcID = "Bear";
            dialogIndex = 0;
            StoryManager.Instance.RegisterNPC("Bear");
        }

        public override void Interact()
        {
        }

        private void ShowOptions(string optionA, string responseA, string optionB, string responseB)
        {
            // TODO: Implement UI for showing options and capturing player's choice.
            Debug.Log($"Option A: {optionA}");
            Debug.Log($"Option B: {optionB}");
        }
    }
}
