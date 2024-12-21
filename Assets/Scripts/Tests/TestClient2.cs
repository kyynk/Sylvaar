using System.Collections.Generic;
using AVG;
using UnityEngine;

namespace Tests
{
    public class TestClient2 : MonoBehaviour
    {
        public List<DialogData> dialogs;
        AVGUIPanel panel;
        Sprite hikari, akane, event01, event02;
        GameObject buttonPrefab;
        private void Start()
        {
            panel = FindAnyObjectByType<AVGUIPanel>();
            hikari = Resources.Load<Sprite>("Textures/Characters/hikari/normal");
            akane = Resources.Load<Sprite>("Textures/Characters/akane/normal");
            event01 = Resources.Load<Sprite>("Textures/Events/event_01");
            event02 = Resources.Load<Sprite>("Textures/Events/event_02");
            buttonPrefab = Resources.Load<GameObject>("Prefabs/UI/ChoiceButton");
            AVGUIManager.Instance.AVGUIHide();
        }

        private void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                AVGUIManager.Instance.AVGUIShow();
                DialogData dialog = dialogs[0];
                AVGUIManager.Instance.AVGUILoadDialog(dialog);
            }
            if (Input.GetKeyDown("2"))
            {
                AVGUIManager.Instance.AVGUIShow();
                DialogData dialog = dialogs[1];
                AVGUIManager.Instance.AVGUILoadDialog(dialog);
            }
            if (Input.GetKeyDown("3"))
            {
                AVGUIManager.Instance.AVGUIShow();
                DialogData dialog = dialogs[2];
                AVGUIManager.Instance.AVGUILoadDialog(dialog);
            }
            if (Input.GetKeyDown("4"))
            {
                AVGUIManager.Instance.AVGUIShow();
                DialogData dialog = dialogs[3];
                AVGUIManager.Instance.AVGUILoadDialog(dialog);
            }
            if (Input.GetKeyDown("5"))
            {
                AVGUIManager.Instance.AVGUIShow();
                DialogData dialog = dialogs[4];
                AVGUIManager.Instance.AVGUILoadDialog(dialog);
            }
            if (Input.GetKeyDown("6"))
            {
                AVGUIManager.Instance.AVGUIHide();
            }
        }
    }
}
