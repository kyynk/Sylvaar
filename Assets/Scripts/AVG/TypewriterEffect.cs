using System;
using KeyboardInput;
using TMPro;
using UnityEngine;

namespace AVG
{
    public class TypewriterEffect : MonoBehaviour
    {
        private enum STATE
        {
            PAUSED,
            TYPING,
            FINISHED
        }

        [SerializeField]
        private STATE state;
        private ITypewriterStrategy currentStrategy;
        private TextMeshProUGUI textComponent;
        private string fullText;
        private float elapsedTime;
        private int currentCharIndex;
        private Action onTypeComplete;
        private KeyboardInputManager inputManager;
        private bool triggerEnter;

        private void Awake()
        {
            textComponent = GetComponent<TextMeshProUGUI>();
            inputManager = FindObjectOfType<KeyboardInputManager>();
            inputManager.evtDialogClick.AddListener(OnDialogClicked);
            // SetStrategy(new NormalTypewriter());
            SetStrategy(new WaveTypewriter());
            GoToState(STATE.PAUSED);
        }

        private void OnDialogClicked(bool pressed)
        {
            if (this.enabled && pressed && state == STATE.TYPING)
            {
                CompleteTyping();
            }
        }

        public void SetStrategy(ITypewriterStrategy strategy)
        {
            currentStrategy = strategy;
        }

        public void StartTyping(string text, Action onComplete = null)
        {
            fullText = text;
            textComponent.text = "";
            currentCharIndex = 0;
            elapsedTime = 0;
            onTypeComplete = onComplete;
            GoToState(STATE.TYPING);
        }

        private void Update()
        {
            switch (state)
            {
                case STATE.PAUSED:
                    break;
                case STATE.TYPING:
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= currentStrategy.GetTypeSpeed())
                    {
                        elapsedTime = 0;
                        currentCharIndex += 1;
                        if (currentCharIndex >= fullText.Length)
                        {
                            GoToState(STATE.FINISHED);
                        }
                        else
                        {
                            textComponent.text = currentStrategy.ProcessText(fullText, currentCharIndex, elapsedTime);
                        }
                    }
                    else
                    {
                        textComponent.text = currentStrategy.PrepareText(fullText, currentCharIndex, elapsedTime);
                    }
                    break;
                case STATE.FINISHED:
                    if (triggerEnter)
                    {
                        // isTyping = false;
                        textComponent.text = currentStrategy.ProcessText(fullText, currentCharIndex, elapsedTime);
                        onTypeComplete?.Invoke();
                        triggerEnter = false;
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        public void CompleteTyping()
        {
            currentCharIndex = fullText.Length + 1;
            GoToState(STATE.FINISHED);
            onTypeComplete?.Invoke();
        }

        public bool IsTyping => state == STATE.TYPING;
        public bool IsFinished => state == STATE.FINISHED;

        private void GoToState(STATE targetState)
        {
            state = targetState;
            triggerEnter = true;
        }
    }
}
