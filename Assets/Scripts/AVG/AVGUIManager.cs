using UnityEngine;

namespace AVG
{
    public class AVGUIManager : Singleton<AVGUIManager>
    {
        private GameObject choicePrefab;
        AVGUIPanel panel;

        protected override void Init()
        {
            panel = FindObjectOfType<AVGUIPanel>();
            choicePrefab = Resources.Load<GameObject>("Prefabs/UI/ChoiceButton");
        }

        public void AVGUIShow()
        {
            panel.gameObject.SetActive(true);
        }

        public void AVGUIHide()
        {
            panel.gameObject.SetActive(false);
        }

        public void AVGUILoadDialog(DialogData dialog)
        {
            Sprite characterImage, eventImage;
            string dialogContent;
            switch (dialog.displayType)
            {
                case "normal":
                    characterImage = Resources.Load<Sprite>(
                        $"Textures/Characters/{dialog.characterName}/{dialog.characterExpression}");
                    dialogContent = dialog.dialogText;
                    panel.ShowCharacter(characterImage, true, dialog.characterPosition);
                    panel.ShowEvent(null, false);
                    panel.ShowDialogBox(dialog.characterName, dialogContent);
                    panel.ShowChoices(null, null, null, false);
                    panel.PlayAudio(dialog.voiceClip);
                    break;
                case "event":
                    eventImage = Resources.Load<Sprite>(
                        $"Textures/Events/{dialog.eventImage}");
                    dialogContent = dialog.dialogText;
                    panel.ShowCharacter(null, false);
                    panel.ShowEvent(eventImage, true);
                    panel.ShowDialogBox("", dialogContent);
                    panel.ShowChoices(null, null, null, false);
                    break;
                case "choice":
                    panel.ShowCharacter(null, false);
                    panel.ShowEvent(null, false);
                    panel.ShowDialogBox(null, null, false);
                    panel.ShowChoices(dialog.choices, dialog.nextSceneIDs, choicePrefab, true);
                    panel.PlayAudio(dialog.voiceClip);
                    AVGMachine.Instance.Pause(true);
                    break;
                case "narration":
                    dialogContent = dialog.dialogText;
                    panel.ShowCharacter(null, false);
                    panel.ShowEvent(null, false);
                    panel.ShowDialogBox("", dialogContent);
                    panel.ShowChoices(null, null, null, false);
                    break;
                default:
                    break;
            }
        }

        public bool IsDialogComplete()
        {
            TypewriterEffect typewriter = panel.GetComponentInChildren<TypewriterEffect>();
            return typewriter.IsFinished;
        }
    }
}