using UnityEngine;
using UnityEngine.Events;

public class DebugInputManager : MonoBehaviour
{
    public UnityEvent<string> evtEquipWeapon;

    // Update is called once per frame
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown("1"))
        {
            evtEquipWeapon?.Invoke("Sword");
        }
        else if (UnityEngine.Input.GetKeyDown("2"))
        {
            evtEquipWeapon?.Invoke("Shield");
        }
        else if (UnityEngine.Input.GetKeyDown("3"))
        {
            evtEquipWeapon?.Invoke("Bomb");
        }
    }
}
