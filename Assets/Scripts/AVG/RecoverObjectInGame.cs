using UnityEngine;

namespace AVG
{
    public class RecoverObjectInGame : MonoBehaviour
    {
        private void Start()
        {
            AVGUIManager.Instance.RecoverPanel();
            AVGMachine.Instance.RecoverSettings();
        }
    }
}
