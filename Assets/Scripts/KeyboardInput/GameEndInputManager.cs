using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace KeyboardInput
{
    public class GameEndInputManager : MonoBehaviour
    {
        private Button restartButton;

        void Start()
        {
            restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
            
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartButtonClicked);
            }
            else
            {
                //Debug.LogError("RestartButton not found!");
            }
        }

        private void OnRestartButtonClicked()
        {
            //Debug.Log("Restart button clicked. Changing game state to InGame.");
            GameManager.Instance.GameStateChange(GameManager.GameState.MainMenu);
        }

    }
}
