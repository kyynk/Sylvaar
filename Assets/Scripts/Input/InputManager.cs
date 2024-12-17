using UnityEngine.Events;
using UnityEngine;

public abstract class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> evtDpadAxis;
    public UnityEvent<bool> evtJump;
    public UnityEvent<bool> evtRun;
    public UnityEvent<bool> evtDialogClick;
    
    protected abstract void CalculateDpadAxis();
    protected abstract void CalculateJump();
    protected abstract void CalculateRun();
    protected abstract void CalculateDialogClick();
    protected abstract void PostProcessDpadAxis();

    private void Update()
    {
        CalculateDpadAxis();
        CalculateJump();
        CalculateRun();
        CalculateDialogClick();
        PostProcessDpadAxis();
    }
}
