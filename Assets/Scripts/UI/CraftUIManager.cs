using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{

    public class CraftUIManager : MonoBehaviour
    {
        public GameObject backpackUI;       // Reference to the backpack UI image
        public GameObject synthesisUI;       // Reference to the synthesis UI image
        public GameObject synthesisBox;       // Reference to the synthesis Box image
        public GameObject plus;       // Reference to the plus image
        public GameObject resultBox;       // Reference to the result Box image
        public GameObject backpackGrid;  // Reference to the backpack grid panel
        public Button synthesisButton;       // Reference to the synthesis button
        public Button craftButton;   // Reference to the craft button
        public Sprite craftIcon;        // Craft button image
        public Sprite closeIcon;           // X button image
        public GameObject itemSlotPrefab;  // Prefab for the item slot
        public int gridSize = 15;       // Number of slots in the grid

        private GameObject selectedSlot;    // Currently selected slot from the backpack
        private GameObject[] craftingSlots; // Two crafting slots
        private string[] craftingItems;     // Tracks items in crafting slots

        // Start is called before the first frame update
        void Start()
        {
            craftingSlots = new GameObject[2]; // Initialize two crafting slots
            craftingItems = new string[2];     // Initialize crafting item identifiers
            backpackUI.SetActive(false);       // Backpack hidden initially
            synthesisUI.SetActive(false);      // Synthesis UI hidden initially
            craftButton.onClick.AddListener(ToggleCraftingUI);
            synthesisButton.onClick.AddListener(SynthesizeItem);
            InitializeBackpack();
        }

        // Toggles the Crafting UI
        void ToggleCraftingUI()
        {
            bool isActive = backpackUI.activeSelf || synthesisUI.activeSelf;
            backpackUI.SetActive(!isActive);
            synthesisUI.SetActive(!isActive);
            craftButton.image.sprite = isActive ? craftIcon : closeIcon;
        }

        // Initializes the Backpack Grid
        void InitializeBackpack()
        {
            foreach (Transform child in backpackGrid.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < gridSize; i++)
            {
                GameObject newSlot = Instantiate(itemSlotPrefab, backpackGrid.transform);
                Image slotImage = newSlot.GetComponent<Image>();
                Button slotButton = newSlot.GetComponent<Button>();
                slotButton.onClick.AddListener(() => OnBackpackSlotClicked(newSlot));
                slotImage.color = new Color(1, 1, 1, 1);
            }
        }

        // Handles Backpack Slot Selection
        void OnBackpackSlotClicked(GameObject slot)
        {
            if (selectedSlot != null)
            {
                selectedSlot.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

            selectedSlot = slot;
            selectedSlot.GetComponent<Image>().color = new Color(1, 1, 0, 0.5f);

            AddToCraftingArea(slot);
        }

        // Adds an item to the crafting area
        void AddToCraftingArea(GameObject slot)
        {
            for (int i = 0; i < craftingSlots.Length; i++)
            {
                if (craftingSlots[i] == null) // Check for an empty crafting slot
                {
                    craftingSlots[i] = slot; // Assign the backpack slot to crafting
                    craftingItems[i] = "Item_" + slot.name; // Example: Assign item identifier
                    UpdateCraftingUI();
                    return;
                }
            }
        }

        // Updates the Crafting Area UI
        void UpdateCraftingUI()
        {
            for (int i = 0; i < craftingSlots.Length; i++)
            {
                Image slotImage = synthesisBox.transform.GetChild(i).GetComponent<Image>();
                if (craftingSlots[i] != null)
                {
                    slotImage.color = new Color(1, 1, 1, 1); // Make slot visible
                    slotImage.sprite = craftingSlots[i].GetComponent<Image>().sprite; // Assign item sprite
                }
                else
                {
                    slotImage.color = new Color(0, 0, 0, 0); // Make slot invisible
                }
            }
        }

        // Synthesize Items
        void SynthesizeItem()
        {
            if (IsValidRecipe(craftingItems[0], craftingItems[1]))
            {
                // Show synthesized item in resultBox
                Image resultImage = resultBox.GetComponent<Image>();
                resultImage.color = new Color(1, 1, 1, 1);
                resultImage.sprite = GetSynthesizedSprite(craftingItems[0], craftingItems[1]);

                // Clear crafting slots
                ClearCraftingArea();
            }
            else
            {
                Debug.Log("Invalid recipe!");
            }
        }

        // Checks if the selected items form a valid recipe
        bool IsValidRecipe(string item1, string item2)
        {
            // Replace this with your actual recipe logic
            return item1 == "Item_1" && item2 == "Item_2";
        }

        // Returns the sprite for the synthesized item
        Sprite GetSynthesizedSprite(string item1, string item2)
        {
            // Replace this with your synthesized item sprite logic
            return Resources.Load<Sprite>("SynthesizedItem");
        }

        // Clears the crafting area
        void ClearCraftingArea()
        {
            for (int i = 0; i < craftingSlots.Length; i++)
            {
                craftingSlots[i] = null;
                craftingItems[i] = null;
            }
            UpdateCraftingUI();
        }
    }
}