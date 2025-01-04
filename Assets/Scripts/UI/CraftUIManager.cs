using System.Collections;
using System.Collections.Generic;
using System;
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
        [SerializeField] GameObject playerBag;

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
            UpdateBackpack();
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
            // Check if the slot is empty
            if (slot.GetComponent<Image>().sprite == null)
            {
                return; // Do nothing if the slot is empty
            }
            if (selectedSlot != null)
            {
                // Reset the previous slot's highlight (keep its content intact)
                selectedSlot.GetComponent<Image>().color = Color.white;
            }

            selectedSlot = slot;
            selectedSlot.GetComponent<Image>().color = new Color(1, 1, 0, 0.5f);

            AddToCraftingArea(slot);
        }

        public void UpdateBackpack()
        {
            Dictionary<string, int> itemCounts = playerBag.GetComponent<PlayerBag>().GetItemCount();
            // Clear all slot visuals
            foreach (Transform child in backpackGrid.transform)
            {
                Image slotImage = child.GetComponent<Image>();
                slotImage.sprite = null;
                slotImage.color = new Color(0, 0, 0, 0); // Make the slot invisible
                TMPro.TextMeshProUGUI quantityText = child.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                if (quantityText != null) quantityText.text = ""; // Clear quantity text
            }

            // Populate slots with items
            int slotIndex = 0;
            foreach (var item in itemCounts)
            {
                string itemName = item.Key;
                int itemQuantity = item.Value;

                // Skip items with zero quantity
                if (itemQuantity <= 0) continue;

                // Iterate over item quantities in stack sizes
                while (itemQuantity > 0 && slotIndex < backpackGrid.transform.childCount)
                {
                    GameObject slot = backpackGrid.transform.GetChild(slotIndex).gameObject;
                    Image slotImage = slot.GetComponent<Image>();
                    TMPro.TextMeshProUGUI quantityText = slot.GetComponentInChildren<TMPro.TextMeshProUGUI>();

                    // Assign item sprite and make slot visible
                    slotImage.sprite = GetItemSprite(itemName);
                    slotImage.color = Color.white;

                    // Display the stack size in the slot
                    int stackSize = Mathf.Min(playerBag.GetComponent<PlayerBag>().GetStackLimit(itemName), itemQuantity);
                    if (quantityText != null)
                    {
                        quantityText.text = stackSize.ToString();
                    }

                    itemQuantity -= stackSize; // Decrease remaining item quantity
                    slotIndex++; // Move to the next slot
                }

                // Stop if we run out of slots
                if (slotIndex >= backpackGrid.transform.childCount)
                {
                    Debug.LogWarning("Not enough slots to display all items.");
                    break;
                }
            }
        }

        public Sprite GetItemSprite(string itemName){
            return Resources.Load<Sprite>("Image/ItemIcon/" + itemName);
        }

        // Adds an item to the crafting area
        void AddToCraftingArea(GameObject slot)
        {
            for (int i = 0; i < craftingSlots.Length; i++)
            {
                if (craftingSlots[i] == null) // Check for an empty crafting slot
                {
                    craftingSlots[i] = slot; // Assign the backpack slot to crafting

                    string itemName = slot.GetComponent<Image>().sprite.name;
                    craftingItems[i] = itemName; // Example: Assign item identifier

                    // Remove the item from the player's inventory
                    playerBag.GetComponent<PlayerBag>().RemoveItem(itemName);

                    UpdateCraftingUI();
                    UpdateBackpack();
                    return;
                }
            }
        }

        void OnCraftingSlotClicked(GameObject slot)
        {
            //FIXME: out of bounds error when slot clicked
            // Check if the slot has an item
            if (slot.GetComponent<Image>().sprite == null)
            {
                return; // Do nothing if the slot is empty
            }

            // Find the index of the slot to remove the item from
            int slotIndex = Array.IndexOf(craftingSlots, slot);

            if (slotIndex >= 0)
            {
                // Remove the item from the player's inventory
                string itemName = craftingItems[slotIndex];
                playerBag.GetComponent<PlayerBag>().AddItem(itemName);

                // Clear the crafting slot
                craftingSlots[slotIndex] = null;
                craftingItems[slotIndex] = null;

                // Update the crafting UI
                UpdateCraftingUI();
                UpdateBackpack();
            }
        }

        // Updates the Crafting Area UI
        void UpdateCraftingUI()
        {
            for (int i = 0; i < craftingSlots.Length; i++)
            {
                Image slotImage = synthesisBox.transform.GetChild(i).GetComponent<Image>();
                Button slotButton = slotImage.GetComponent<Button>();
                slotButton.onClick.RemoveAllListeners(); // Remove any previous listeners
                slotButton.onClick.AddListener(() => OnCraftingSlotClicked(synthesisBox.transform.GetChild(i).gameObject));

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
                playerBag.GetComponent<PlayerBag>().AddItem(resultImage.sprite.name);
                UpdateBackpack();
            }
            else
            {
                //Debug.Log("Invalid recipe!");
            }
        }

        // Checks if the selected items form a valid recipe
        bool IsValidRecipe(string item1, string item2)
        {
            // TODO: Replace this with your actual recipe logic
            return item1 == "wood" && item2 == "wood";
        }

        // Returns the sprite for the synthesized item
        Sprite GetSynthesizedSprite(string item1, string item2)
        {
            // TODO: Replace this with your synthesized item sprite logic
            return Resources.Load<Sprite>("Image/ItemIcon/" + "shield");
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