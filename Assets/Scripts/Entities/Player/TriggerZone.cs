using AVG;
using UnityEngine;

namespace Player
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] protected string scriptPath;
        [SerializeField] private bool isRetriggerable = false;
        private bool hasTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")
                && AVGMachine.Instance.IsFinished()
                && (!hasTriggered || isRetriggerable))
            {
                hasTriggered = true;
                AVGMachine.Instance.LoadFromCSV(scriptPath);
                AVGMachine.Instance.Play();
            }
        }
    }
}