using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Entities.Player;

namespace UI
{
    public class BackpackUIManager : MonoBehaviour
    {
        public GameObject backpackUI;       // Reference to the backpack UI image
        public GameObject backpackGrid;  // Reference to the backpack grid panel
        public Button backpackButton;   // Reference to the backpack button
        public Sprite backpackIcon;        // Backpack button image
        public Sprite closeIcon;           // X button image
        public GameObject itemSlotPrefab;  // Prefab for the item slot
        public int gridSize = 15;       // Number of slots in the grid
        private GameObject selectedSlot; // Currently selected slot
        [SerializeField] GameObject playerBag;

        void Start()
        {
            if (playerBag == null)
            {
                playerBag = GameObject.FindWithTag("Player"); // Assuming the PlayerBag GameObject is tagged as "Player"
            }

            backpackGrid.SetActive(false);
            backpackUI.SetActive(false);
            backpackButton.onClick.AddListener(ToggleBackpack);
            InitializeBackpack();
            InitializeBackpackGrid();
        }

        // Toggles the visibility of the backpack grid
        void ToggleBackpack()
        {
            bool isBackpackActive = backpackUI.activeSelf;
            UpdateBackpack();

            backpackGrid.SetActive(!isBackpackActive);
            backpackUI.SetActive(!isBackpackActive);
            backpackButton.image.sprite = isBackpackActive ? backpackIcon : closeIcon;

            if (!isBackpackActive) // If opening backpack
            {
                backpackButton.image.sprite = closeIcon; // Change to X button
                SelectFirstSlot();
            }
            else // If closing backpack
            {
                backpackButton.image.sprite = backpackIcon; // Change to backpack icon
            }
        }

        void SelectFirstSlot()
        {
            if (backpackGrid.transform.childCount > 0)
            {
                GameObject firstSlot = backpackGrid.transform.GetChild(0).gameObject;

                if (selectedSlot != null)
                {
                    // Reset the previously selected slot's highlight (keep its content intact)
                    selectedSlot.GetComponent<Image>().color = Color.white;
                }

                // Highlight the first slot
                selectedSlot = firstSlot;
                selectedSlot.GetComponent<Image>().color = new Color(1, 1, 0, 0.5f); // Highlight color
            }
        }
    
        void InitializeBackpackGrid()
        {
            GridLayoutGroup gridLayout = backpackGrid.GetComponent<GridLayoutGroup>();

            if (gridLayout != null)
            {
                // Set grid layout properties
                gridLayout.cellSize = new Vector2(180, 180);   // Slot size
                gridLayout.spacing = new Vector2(46, 46);   // Horizontal and vertical spacing
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayout.constraintCount = 5;            // 5 columns for a 5x3 grid
            }
        }

        // Initializes the backpack grid with item slots
        void InitializeBackpack()
        {
            // Clear existing slots
            foreach (Transform child in backpackGrid.transform)
            {
                Destroy(child.gameObject);
            }

            // Calculate the grid dimensions
            int gridDimension = Mathf.CeilToInt(Mathf.Sqrt(gridSize));
            GridLayoutGroup gridLayout = backpackGrid.GetComponent<GridLayoutGroup>();

            if (gridLayout != null)
            {
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayout.constraintCount = gridDimension;
            }

            // Create the slots
            for (int i = 0; i < gridSize; i++)
            {
                GameObject newSlot = Instantiate(itemSlotPrefab, backpackGrid.transform);
                Image slotImage = newSlot.GetComponent<Image>();
                newSlot.GetComponent<Button>().onClick.AddListener(() => OnItemSlotClicked(newSlot));

                // Make the slot initially invisible
                slotImage.color = new Color(0, 0, 0, 0);
            }
        }

        // Handles item slot click
        void OnItemSlotClicked(GameObject slot)
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

            // Highlight the selected slot
            selectedSlot = slot;
            selectedSlot.GetComponent<Image>().color = new Color(1, 1, 0, 0.5f); // Highlight color
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
    }
}