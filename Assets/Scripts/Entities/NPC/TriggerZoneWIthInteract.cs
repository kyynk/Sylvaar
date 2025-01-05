using AVG;
using UnityEngine;

namespace Entities.NPC
{
    public class TriggerZoneWithInteract : MonoBehaviour
    {
        [SerializeField] protected string scriptPath;
        [SerializeField] private bool isRetriggerable = false;
        private bool hasTriggered = false;

        public void TriggerAVG()
        {
            if (AVGMachine.Instance.IsFinished() && (!hasTriggered || isRetriggerable))
            {
                hasTriggered = true;
                //Debug.Log("Trigger AVG");
                AVGMachine.Instance.LoadFromCSV(scriptPath);
                AVGMachine.Instance.Play();
            }
        }
    }
}