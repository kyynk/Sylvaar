using UnityEngine;
using UnityEngine.UI;

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

        void Start()
        {
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
                    // Reset the previously selected slot's highlight
                    selectedSlot.GetComponent<Image>().color = Color.white;
                }

                // Highlight the first slot
                selectedSlot = firstSlot;
                selectedSlot.GetComponent<Image>().color = Color.yellow;
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
                newSlot.GetComponent<Button>().onClick.AddListener(() => OnItemSlotClicked(newSlot));
            }
        }

        // Handles item slot click
        void OnItemSlotClicked(GameObject slot)
        {
            if (selectedSlot != null)
            {
                // Reset previous slot's highlight
                selectedSlot.GetComponent<Image>().color = Color.white;
            }

            // Highlight the selected slot
            selectedSlot = slot;
            selectedSlot.GetComponent<Image>().color = Color.yellow;
        }
    }
}