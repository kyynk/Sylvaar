using UnityEngine;

namespace AVG
{
    public class AVGChoiceButton : MonoBehaviour
    {
        public int targetDialogID;

        public void BranchDialog()
        {
            AVGMachine.Instance.SetNextDialog(targetDialogID);
            AVGMachine.Instance.Pause(false);
        }
    }
}