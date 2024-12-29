using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;

public class GameStartInputManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Game Started");
            GameManager.Instance.GameStateChange(GameManager.GameState.InGame);
        }       
    }
}
