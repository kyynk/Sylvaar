using Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public class BearNPC : NPCInteractable
    {
        public override void Interact()
        {
            Debug.Log("Hey, little squirrel! Are you looking for something special?");
            Debug.Log("I just found a beautiful stone that might help you. But I'm really hungry right now.");

            ShowOptions(
                "Sure!", "Thank you so much, I'll leave this to you!",
                "No!", "Alright then, I'll see if someone else can help me."
            );
        }

        private void ShowOptions(string optionA, string responseA, string optionB, string responseB)
        {
            // TODO: Implement UI for showing options and capturing player's choice.
            Debug.Log($"Option A: {optionA}");
            Debug.Log($"Option B: {optionB}");
        }
    }
}
