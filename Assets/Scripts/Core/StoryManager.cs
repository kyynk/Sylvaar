using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class StoryManager : MonoBehaviour
    {
        public static StoryManager Instance;

        private Dictionary<string, QuestStatus> questStatuses = new Dictionary<string, QuestStatus>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void TriggerQuest(string quest)
        {
            if (GetQuestStatus(quest) == QuestStatus.Completed)
            {
                //Debug.Log($"Quest {quest} is completed");
            }
            else
            {
                UpdateQuestStatus(quest, QuestStatus.Completed);
            }
        }

        public void RegisterNPC(string npcID)
        {
            if (!questStatuses.ContainsKey(npcID))
            {
                questStatuses[npcID] = QuestStatus.NotStarted;
                //Debug.Log($"NPC Successful Registered : {npcID}");
            }
        }

        public void UpdateQuestStatus(string npcID, QuestStatus newStatus)
        {
            if (questStatuses.ContainsKey(npcID))
            {
                questStatuses[npcID] = newStatus;
                //Debug.Log($"quest status update: {npcID} - {newStatus}");
            }
            else
            {
                //Debug.LogWarning($"fail of update quest，cant find  NPC: {npcID}");
            }
        }

        public QuestStatus GetQuestStatus(string npcID)
        {
            return questStatuses.TryGetValue(npcID, out var status) ? status : QuestStatus.Unknown;
        }

    }

    public enum QuestStatus
    {
        Unknown,
        NotStarted,
        InProgress,
        Completed
    }
}