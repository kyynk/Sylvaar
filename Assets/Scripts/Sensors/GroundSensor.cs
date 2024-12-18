using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sensors
{
    public class GroundSensor : MonoBehaviour
    {
        public UnityEvent<bool> evtGround;
        [SerializeField]
        private List<Collider> colliders = new List<Collider>();

        private void Update()
        {
            evtGround.Invoke((colliders.Count > 0) ? true : false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ground")
            {
                colliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            colliders.Remove(other);
        }
    }
}
