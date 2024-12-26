using Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class Raccoon : NPCInteractable
    {
        public override void Interact()
        {
            Debug.Log("Hey, squirrel! Are you looking for the Acorn Relic?");
            Debug.Log("I have a key fragment that can unlock it, but I need your help to wash something.");

            ShowOptions(
                "Sure!", "That's great! I have so many things to wash, and I can't finish it all. Thank you so much!",
                "No!", "QQ, alright then..."
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

