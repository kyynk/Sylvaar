using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class PlayerBag : MonoBehaviour
{
    [SerializeField] GameObject backpackUIManager;
    [SerializeField] GameObject craftUIManager;
    Dictionary<string, int> stackLimit;
    Dictionary<string, int> itemCount;



    // Start is called before the first frame update
    string filePath; // Path to the CSV file

    void Start()
    {
        // Initialize dictionaries
        stackLimit = new Dictionary<string, int>
        {
            { "stick", 10 },
            { "wood", 10 },
            { "stone", 10 },
            { "shield", 1 },
            { "sword", 1 },
            { "bomb", 5 }
        };

        itemCount = new Dictionary<string, int>
        {
            { "stick", 0 },
            { "wood", 0 },
            { "stone", 0 },
            { "shield", 0 },
            { "sword", 0 },
            { "bomb", 0 }
        };

        // Set file path
        filePath = Path.Combine(Application.streamingAssetsPath, "PlayerItems/items.csv");

        // Load item counts from CSV
        LoadItemCounts();
    }

    void Update(){
        LoadItemCounts();
    }

    public Dictionary<string, int> GetItemCount(){
        return itemCount;
    }

    public int GetStackLimit(string itemName){
        return stackLimit[itemName];
    }

    // Loads item counts from the CSV file
    void LoadItemCounts()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"CSV file not found at: {filePath}");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length < 2) return; // Ensure we have header and data rows

        string[] items = lines[0].Split(','); // Header row (item names)
        string[] counts = lines[1].Split(','); // Data row (item counts)

        for (int i = 0; i < items.Length; i++)
        {
            if (itemCount.ContainsKey(items[i]))
            {
                itemCount[items[i]] = int.Parse(counts[i]);
            }
        }

        // Debug.Log("Item counts loaded from CSV.");
        // Debug.Log(itemCount["wood"]);
    }

    // Saves item counts to the CSV file
    void SaveItemCounts()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header row
            writer.WriteLine(string.Join(",", itemCount.Keys));

            // Write data row
            writer.WriteLine(string.Join(",", itemCount.Values));
        }

        Debug.Log("Item counts saved to CSV.");
        Debug.Log(itemCount["wood"]);
    }

    // Adds an item to the inventory
    public bool AddItem(string item, int amount = 1)
    {
        if (!itemCount.ContainsKey(item))
        {
            Debug.LogError($"Item '{item}' does not exist in inventory.");
            return false;
        }

        int currentCount = itemCount[item];
        int maxCount = stackLimit[item];

        if (currentCount + amount > maxCount)
        {
            Debug.LogWarning($"Cannot add {amount} '{item}' - exceeds stack limit.");
            return false;
        }

        itemCount[item] += amount;
        SaveItemCounts();
        return true;
    }

    // Removes an item from the inventory
    public bool RemoveItem(string item, int amount = 1)
    {
        if (!itemCount.ContainsKey(item))
        {
            Debug.LogError($"Item '{item}' does not exist in inventory.");
            return false;
        }

        int currentCount = itemCount[item];
        if (currentCount < amount)
        {
            Debug.LogWarning($"Cannot remove {amount} '{item}' - not enough in inventory.");
            return false;
        }

        itemCount[item] -= amount;
        SaveItemCounts();
        return true;
    }
}
