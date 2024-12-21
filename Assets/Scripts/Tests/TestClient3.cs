using AVG;
using UnityEngine;

namespace Tests
{
    public class TestClient3 : MonoBehaviour
    {
        private void Start()
        {
            AVGMachine.Instance.Stop();
        }

        private void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                AVGMachine.Instance.LoadFromCSV("AVGScripts/scene1.csv");
                AVGMachine.Instance.Play();
            }
        }
    }
}