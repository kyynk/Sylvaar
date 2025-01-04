using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace KeyboardInput
{
    public class GameStartInputManager : MonoBehaviour
    {
        private Button startButton;

        void Start()
        {
            startButton = GameObject.Find("StartButton").GetComponent<Button>();

            if (startButton != null)
            {
                startButton.onClick.AddListener(OnStartButtonClicked);
            }
            else
            {
                Debug.LogError("StartButton not found!");
            }
        }

        private void OnStartButtonClicked()
        {
            Debug.Log("Start button clicked. Changing game state to InGame.");
            GameManager.Instance.GameStateChange(GameManager.GameState.InGame);
        }
    }
}
