using System.Collections.Generic;
using AVG;
using UnityEngine;

namespace Entities.NPC
{
    public class TriggerZoneWithInteract : MonoBehaviour
    {
        [SerializeField] protected List<string> scriptsPath;
        [SerializeField] private bool isRetriggerable = false;
        [SerializeField] private string npc;
        [SerializeField] private QuestSystem questSystem;
        private bool hasTriggered = false;

        public void TriggerAVG()
        {
            int scriptID = questSystem.GetScriptID(npc);
            if (AVGMachine.Instance.IsFinished()
                && (!hasTriggered || isRetriggerable)
                && scriptID != -1)
            {
                hasTriggered = true;
                // Debug.Log("Trigger AVG");
                AVGMachine.Instance.LoadFromCSV(scriptsPath[scriptID], npc);
                AVGMachine.Instance.Play();
            }
        }
    }
}