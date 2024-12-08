using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;
    [SerializeField] private List<Slot_UI> backpackSlots; // References to backpack UI slots

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;
    [SerializeField] private List<Slot_UI> toolbarSlots; // References to toolbar UI slots

    private void Awake()
    {
        // Initialize backpack and toolbar
        backpack = new Inventory(backpackSlotCount);
        Debug.Log($"Toolbar initialized with {toolbar.slots.Count} slots. Backpack initialized with {backpack.slots.Count} slots.");

        toolbar = new Inventory(toolbarSlotCount);
        

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);

        // Initialize UI slots with inventory slots
        InitializeUISlots(backpack, backpackSlots);
        InitializeUISlots(toolbar, toolbarSlots);
    }

    // Add an item to the specified inventory
    public bool Add(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item); // Add the item to the inventory
            Debug.Log($"Item {item.data.itemName} successfully added to {inventoryName}.");
            return true;
        }
        else
        {
            Debug.LogWarning($"Inventory {inventoryName} does not exist.");
            return false;
        }
    }

    // Get inventory by name
    public Inventory GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }

        Debug.LogWarning($"Inventory {inventoryName} does not exist.");
        return null;
    }

    // Initialize UI slots with inventory slots
    private void InitializeUISlots(Inventory inventory, List<Slot_UI> uiSlots)
    {
        if (uiSlots.Count != inventory.slots.Count)
        {
            Debug.LogError("UI slots count does not match inventory slots count!");
            return;
        }

        for (int i = 0; i < uiSlots.Count; i++)
        {
            inventory.slots[i] = new Inventory.Slot(i); // Initialize with correct IDs
            Debug.Log($"Initializing slot {i} for inventory. Slot reference: {inventory.slots[i]?.itemReference?.data.itemName}");

            uiSlots[i].slotID = i; // Set slot ID for UI
            uiSlots[i].inventory = inventory; // Link to inventory

            // Add debug logs to confirm slot data
            if (inventory.slots[i].itemReference != null)
            {
                Debug.Log($"Initialized slot {i} with item {inventory.slots[i].itemReference.data.itemName}");
            }
            else
            {
                Debug.Log($"Slot {i} is empty.");
            }

            uiSlots[i].SetEmpty(); // Clear UI slot initially
        }
    }
}
