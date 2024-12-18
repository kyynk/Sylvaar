using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace AVG
{
    public class AVGUIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private GameObject choicePanel;
        [SerializeField] private Image characterImage;
        [SerializeField] private Image eventImage;
        [SerializeField] private Image dialogBoxImage;
        [SerializeField] private Image namePanelImage;
        [SerializeField] private TextMeshProUGUI dialogBoxText;
        [SerializeField] private TextMeshProUGUI characterNameText;

        public void ShowCharacter(Sprite image,
            bool visibility = true, string position = "center")
        {
            characterImage.enabled = visibility;
            characterImage.sprite = image;
            characterImage.preserveAspect = true;
            switch (position)
            {
                case "left":
                    characterImage.rectTransform.anchoredPosition
                        = new Vector2(-300, 0);
                    break;
                case "right":
                    characterImage.rectTransform.anchoredPosition
                        = new Vector2(300, 0);
                    break;
                default:
                    characterImage.rectTransform.anchoredPosition
                        = new Vector2(0, 0);
                    break;
            }
            UIImageFader fader = characterImage.GetComponentInChildren<UIImageFader>();
            fader.TriggerFade(true, true);
        }

        public void ShowEvent(Sprite image, bool visibility = true)
        {
            eventImage.enabled = visibility;
            eventImage.sprite = image;
            UIImageFader fader = eventImage.GetComponentInChildren<UIImageFader>();
            fader.TriggerFade(true, true);
        }

        public void ShowDialogBox(string nameContent,
            string dialogContent, bool visibility = true)
        {
            dialogBoxImage.enabled = visibility;
            namePanelImage.enabled = visibility;
            dialogBoxText.enabled = visibility;
            characterNameText.text = nameContent;
            // dialogBoxText.text = dialogContent;
            TypewriterEffect typewriter = GetComponentInChildren<TypewriterEffect>();
            if (typewriter != null)
            {
                typewriter.enabled = visibility;
                typewriter.StartTyping(dialogContent);
            }
        }

        public void ShowChoices(List<string> choices, List<int> nextSceneIDs,
            GameObject buttonPrefab, bool visibility)
        {
            choicePanel.SetActive(visibility);
            foreach (Transform button in choicePanel.transform)
            {
                Destroy(button.gameObject);
            }

            if (buttonPrefab == null)
            {
                return;
            }

            for (int i = 0; i < choices.Count; i++)
            {
                GameObject button = Instantiate(buttonPrefab);
                button.transform.parent = choicePanel.transform;
                button.transform.localScale = Vector3.one;
                button.GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
                var choiceButton = button.GetComponentInChildren<AVGChoiceButton>();
                choiceButton.targetDialogID = nextSceneIDs[i];
                button.GetComponentInChildren<Button>().onClick.AddListener( ()=> choiceButton.BranchDialog() );
            }
        }

        public void PlayAudio(string voiceClip)
        {
            var voice = Resources.Load<AudioClip>($"Audio/{voiceClip}");
            if (voice != null)
            {
                var aSource = GetComponent<AudioSource>();
                if (aSource == null)
                {
                    aSource = gameObject.AddComponent<AudioSource>();
                }
                aSource.Stop();
                aSource.clip = voice;
                aSource.Play();
            }
        }
    }
}
