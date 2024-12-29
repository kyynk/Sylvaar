using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class InGaneInputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Good End, Game Over");
            GameManager.Instance.GameStateChange(GameManager.GameState.GoodEnd);
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Bad End, Game Over");
            GameManager.Instance.GameStateChange(GameManager.GameState.BadEnd);
        }
    }
}
