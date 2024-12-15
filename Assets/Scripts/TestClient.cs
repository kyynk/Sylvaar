using UnityEngine;
using AVG;
using System.Collections.Generic;

public class TestClient : MonoBehaviour
{
    private AVGUIPanel panel;
    private Sprite hikari, akane, event01, event02;
    private GameObject buttonPrefab;

    private void Start()
    {
        panel = FindObjectOfType<AVGUIPanel>();
        hikari = Resources.Load<Sprite>("Textures/Characters/hikari/normal");
        akane = Resources.Load<Sprite>("Textures/Characters/akane/normal");
        event01 = Resources.Load<Sprite>("Textures/Events/event_01");
        event02 = Resources.Load<Sprite>("Textures/Events/event_02");
        buttonPrefab = Resources.Load<GameObject>("Prefabs/UI/ChoiceButton");
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            panel.ShowCharacter(hikari);
        }

        if (Input.GetKeyDown("2"))
        {
            panel.ShowCharacter(akane, position:"left");
        }

        if (Input.GetKeyDown("3"))
        {
            panel.ShowCharacter(null, false);
        }

        if (Input.GetKeyDown("4"))
        {
            panel.ShowDialogBox("akane", "Hello world.");
        }

        if (Input.GetKeyDown("5"))
        {
            panel.ShowDialogBox("hikari", "How are you?");
        }

        if (Input.GetKeyDown("6"))
        {
            panel.ShowDialogBox(null, null, false);
        }

        if (Input.GetKeyDown("7"))
        {
            panel.ShowEvent(event01);
        }

        if (Input.GetKeyDown("8"))
        {
            panel.ShowEvent(event02);
        }

        if (Input.GetKeyDown("9"))
        {
            panel.ShowEvent(null, false);
        }

        if (Input.GetKeyDown("0"))
        {
            panel.ShowChoices(new List<string> { "Yes", "No" }, new List<int> { 0, 0 }, buttonPrefab, true);
        }

        if (Input.GetKeyDown("-"))
        {
            panel.ShowChoices(null, null, null, false);
        }
    }
}
