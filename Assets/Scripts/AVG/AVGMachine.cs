using System.Collections.Generic;
using System.IO;
using System.Linq;
using Input;
using UnityEngine;

namespace AVG
{
    public class AVGMachine : Singleton<AVGMachine>
    {
        private enum STATE
        {
            PAUSED,
            RUNNING,
            FINISHED
        }

        private STATE state;
        private bool triggerEnter;
        private int currentID;
        private InputManager inputManager;
        [SerializeField] private List<DialogData> dialogs;

        protected override void Init()
        {
            currentID = 0;
            GoToState(STATE.FINISHED);
            inputManager = FindAnyObjectByType<InputManager>();
            inputManager.evtDialogClick.AddListener(OnDialogClicked);
        }

        private void OnDialogClicked(bool pressed)
        {
            if (state == STATE.RUNNING)
            {
                if (!AVGUIManager.Instance.IsDialogComplete())
                {
                    return;
                }

                DialogData dialog = GetNextDialog();
                if (pressed)
                {
                    if (dialog != null)
                    {
                        SetNextDialog(dialog);
                    }
                    else
                    {
                        GoToState(STATE.FINISHED);
                    }
                }
            }
        }

        private void Update()
        {
            switch (state)
            {
                case STATE.PAUSED:
                    break;
                case STATE.RUNNING:
                    if (triggerEnter)
                    {
                        AVGUIManager.Instance.AVGUIShow();
                    }

                    break;
                case STATE.FINISHED:
                    if (triggerEnter)
                    {
                        AVGUIManager.Instance.AVGUIHide();
                    }
                    break;
                default:
                    break;
            }
        }

        private void GoToState(STATE targetState)
        {
            state = targetState;
            triggerEnter = true;
        }

        public void LoadFromCSV(string filePath)
        {
            dialogs = new List<DialogData>();
            var path = Path.Combine(Application.streamingAssetsPath, filePath);
            var lines = File.ReadAllLines(path).Skip(1); // Skip header
            foreach (var line in lines)
            {
                var values = line.Split(',');
                var data = new DialogData
                {
                    id = int.Parse(values[0]),
                    characterName = values[1],
                    characterExpression = values[2],
                    characterPosition = values[3],
                    eventImage = values[4],
                    soundEffect = values[5],
                    dialogText = values[6],
                    choices = ParseChoices(values[7]),
                    nextSceneIDs = ParseNextSceneIds(values[8], values[0]),
                    animation = values[9],
                    voiceClip = values[10],
                    displayType = values[11]
                };
                dialogs.Add(data);
            }
            currentID = dialogs[0].id;
        }

        public void Play()
        {
            AVGUIManager.Instance.AVGUILoadDialog(GetCurrentDialog());
            GoToState(STATE.RUNNING);
        }

        public void Stop(bool hide = true)
        {
            if (hide)
            {
                AVGUIManager.Instance.AVGUIHide();
            }
            GoToState(STATE.FINISHED);
        }

        public void Pause(bool value)
        {
            if (value)
            {
                GoToState(STATE.PAUSED);
            }
            else
            {
                GoToState(STATE.RUNNING);
            }
        }

        private List<string> ParseChoices(string choicesStr)
        {
            if (string.IsNullOrEmpty(choicesStr) || choicesStr == "[]")
            {
                return new List<string>();
            }
            return choicesStr.Trim('[', ']').Split('|').ToList();
        }

        private List<int> ParseNextSceneIds(string nextSceneIDsStr, string currentSceneID)
        {
            if (string.IsNullOrEmpty(nextSceneIDsStr))
            {
                return new List<int>() { int.Parse(currentSceneID) + 1 };
            }
            print(nextSceneIDsStr);
            return nextSceneIDsStr.Split('|').Select(int.Parse).ToList();
        }

        private DialogData GetCurrentDialog()
        {
            return currentID > 0 ? GetDialogById(currentID) : null;
        }

        private DialogData GetNextDialog()
        {
            DialogData nextDialog = GetDialogById(GetCurrentDialog().nextSceneIDs[0]);
            return nextDialog;
        }

        private DialogData GetDialogById(int id)
        {
            return dialogs?.Find(d => d.id == id);
        }

        internal void SetNextDialog(DialogData nextDialog)
        {
            if (nextDialog != null)
            {
                currentID = nextDialog.id;
                AVGUIManager.Instance.AVGUILoadDialog(nextDialog);
            }
            else
            {
                GoToState(STATE.FINISHED);
            }
        }

        internal void SetNextDialog(int id)
        {
            DialogData nextDialog = GetDialogById(id);
            SetNextDialog(nextDialog);
        }

        public bool IsFinished()
        {
            return (state == STATE.FINISHED);
        }
    }
}