using UnityEngine;

public class KeyboardInputManager : InputManager
{
    private Vector3 axis;
    private bool jump;
    private bool run;
    private bool dialogClick;
    
    protected override void CalculateDpadAxis()
    {
        axis = Vector3.zero;
        if (Input.GetKey("w"))
        {
            axis.z = 1.0f;
        }
        if (Input.GetKey("s"))
        {
            axis.z = -1.0f;
        }
        if (Input.GetKey("d"))
        {
            axis.x = 1.0f;
        }
        if (Input.GetKey("a"))
        {
            axis.x = -1.0f;
        }

        evtDpadAxis?.Invoke(axis);
    }

    protected override void CalculateJump()
    {
        jump = Input.GetKeyDown("space");
        evtJump?.Invoke(jump);
    }

    protected override void CalculateRun()
    {
        run = Input.GetKey("right shift");
        evtRun?.Invoke(run);
    }

    protected override void CalculateDialogClick()
    {
        dialogClick = Input.GetKeyDown("mouse 0");
        evtDialogClick?.Invoke(dialogClick);
    }

    protected override void PostProcessDpadAxis()
    {
        
    }
}
