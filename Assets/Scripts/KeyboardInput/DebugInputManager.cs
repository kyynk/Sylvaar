using UnityEngine;
using UnityEngine.Events;

namespace KeyboardInput
{
    public class DebugInputManager : MonoBehaviour
    {
        public UnityEvent<string> evtEquipWeapon;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                evtEquipWeapon?.Invoke("Sword");
            }
            else if (Input.GetKeyDown("2"))
            {
                evtEquipWeapon?.Invoke("Shield");
            }
            else if (Input.GetKeyDown("3"))
            {
                evtEquipWeapon?.Invoke("Bomb");
            }
        }
    }

}
