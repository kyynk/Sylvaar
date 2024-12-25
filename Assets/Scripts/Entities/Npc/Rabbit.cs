using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class RabbitNPC : NPCInteractable
    {
        public override void Interact()
        {
            Debug.Log("Hey, squirrel! Want to hear a secret?");
            Debug.Log("There's a golden acorn deep in the forest, and I know how to find it!");

            ShowOptions(
                "Sure!", "Hehe, great! Be quick!",
                "No!", "Hmph! Fine, don't help then."
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

