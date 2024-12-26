using Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public class MonkeyNPC : NPCInteractable
    {
        public override void Interact()
        {
            Debug.Log("Hey there, little one! I heard you're looking for some relic, right?");
            Debug.Log("I happen to have a key map fragment, but I won't give it to you just yet.");

            ShowOptions(
                "Sure!", "Great, let's get moving!",
                "No!", "Hmph! You'll regret this."
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
