using AVG;
using UnityEngine;

namespace Entities.NPC
{
    public class TriggerZoneWithCollider : MonoBehaviour
    {
        [SerializeField] protected string scriptPath;
        [SerializeField] private bool isRetriggerable = false;
        [SerializeField] private string npc;
        private bool hasTriggered = false;

        // This is Trigger On Collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")
                && AVGMachine.Instance.IsFinished()
                && (!hasTriggered || isRetriggerable))
            {
                hasTriggered = true;
                AVGMachine.Instance.LoadFromCSV(scriptPath, npc);
                AVGMachine.Instance.Play();
            }
        }
    }
}