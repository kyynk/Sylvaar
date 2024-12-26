using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{ 
    public class StoryManager : MonoBehaviour
    {
        // track the status of each quest
        private Dictionary<string, bool> questStatus = new Dictionary<string, bool>();

    
        private void Start()
        {
            InitializeQuests();
        }

        private void InitializeQuests()
        {
            questStatus["RaccoonTask"] = false; 
            questStatus["BearTask"] = false;   
            questStatus["RabbitTask"] = false; 
            questStatus["MonkeyTask"] = false; 
        }

        // update the status of a quest
        public void UpdateQuestStatus(string questName, bool status)
        {
            if (questStatus.ContainsKey(questName))
            {
                questStatus[questName] = status;
                Debug.Log($"Quest '{questName}' updated to: {status}");
                CheckStoryProgress();
            }
            else
            {
                Debug.LogWarning($"Quest '{questName}' does not exist.");
            }
        }

        // Check if all quests are completed
        private void CheckStoryProgress()
        {
            if (AllQuestsCompleted())
            {
                Debug.Log("All quests are completed! The player can now access the Acorn Relic.");
                TriggerFinalEvent();
            }
        }

        private bool AllQuestsCompleted()
        {
            foreach (var status in questStatus.Values)
            {
                if (!status) return false;
            }
            return true;
        }

        private void TriggerFinalEvent()
        {
            // example: open the hidden cave
            Debug.Log("The hidden cave is now accessible!");
            // here to trigger the final event
        }

        // quest status getter
        public bool IsQuestCompleted(string questName)
        {
            return questStatus.ContainsKey(questName) && questStatus[questName];
        }
    }
}