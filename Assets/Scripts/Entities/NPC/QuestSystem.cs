using System;
using System.Collections.Generic;
using AVG;
using Core;
using UnityEngine;

namespace Entities.NPC
{
    public class QuestSystem : MonoBehaviour
    {
        private List<int> npcScripts = new List<int>(); // 0 bear, 1 rabbit, 2 monkey, 3 raccoon

        private void Start()
        {
            npcScripts.Add(0);
            npcScripts.Add(0);
            npcScripts.Add(0);
            npcScripts.Add(0);
        }

        private void Update()
        {
            if (AVGMachine.Instance.IsFinished() && AVGMachine.Instance.GetFinalNPC() != "")
            {
                string npc = AVGMachine.Instance.GetFinalNPC();
                int npcID = GetScriptsCount(npc);
                if ((AVGMachine.Instance.GetFinalID() == 1003 && npcID == 0)
                    || (AVGMachine.Instance.GetFinalID() == 1004 && npcID == 1)
                    || (AVGMachine.Instance.GetFinalID() == 1004 && npcID == 2)
                    || (AVGMachine.Instance.GetFinalID() == 1003 && npcID == 3))
                {
                    StoryManager.Instance.TriggerQuest(npc, QuestStatus.InProgress);
                    npcScripts[npcID] = 1;
                }
                else if ((AVGMachine.Instance.GetFinalID() == 1004 && npcID == 0)
                         || (AVGMachine.Instance.GetFinalID() == 1005 && npcID == 1)
                         || (AVGMachine.Instance.GetFinalID() == 1005 && npcID == 2)
                         || (AVGMachine.Instance.GetFinalID() == 1004 && npcID == 3))
                {
                    StoryManager.Instance.TriggerQuest(npc, QuestStatus.Failed);
                    npcScripts[npcID] = -1;
                }
                else if (AVGMachine.Instance.GetFinalID() == 3001)
                {
                    StoryManager.Instance.TriggerQuest(npc, QuestStatus.Completed);
                    npcScripts[npcID] = -1;
                }

                AVGMachine.Instance.ResetFinal();
            }
        }

        private int GetScriptsCount(string npc)
        {
            switch (npc)
            {
                case "bear":
                    return 0;
                case "rabbit":
                    return 1;
                case "monkey":
                    return 2;
                case "raccoon":
                    return 3;
                default:
                    return 0;
            }
        }

        public int GetScriptID(string npc)
        {
            return npcScripts[GetScriptsCount(npc)];
        }

        public void SetScriptsID(string npc, int scriptCount)
        {
            int npcID = GetScriptsCount(npc);
            npcScripts[npcID] = scriptCount;
        }
    }
}
