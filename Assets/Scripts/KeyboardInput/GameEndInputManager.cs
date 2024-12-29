using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class GameEndInputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Game Restarted");
            GameManager.Instance.GameStateChange(GameManager.GameState.MainMenu);
        }
    }
}
