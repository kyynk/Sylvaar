using AVG;
using UnityEngine;

namespace UI
{
    public class SceneSetup : MonoBehaviour
    {
        private void Setup()
        {
            AVGMachine.Instance.Stop();
        }
    }
}
