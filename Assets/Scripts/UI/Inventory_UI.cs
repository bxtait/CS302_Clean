using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public string inventoryName;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;

    private Inventory inventory;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Start()
    {
        inventory = GameManager.instance.player.inventoryManager.inventoryByName[inventoryName]; // Directly access the inventory
        SetupSlots();
        Refresh();
    }

    public void Refresh()
    {
        if (slots.Count == inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (!inventory.slots[i].IsEmpty)
                {
                    slots[i].SetItem(inventory.slots[i]); // Pass the Inventory.Slot object to Slot_UI
                    Debug.Log($"Slot {i} contains item {inventory.slots[i].itemName} with reference {inventory.slots[i].itemReference?.data.itemName}");

                }
                else
                {
                    slots[i].SetEmpty();
                    Debug.Log($"Slot {i} is empty.");
                }
            }
        }
    }

    public void Remove()
    {
        // Retrieve the item reference from the Inventory
        var inventorySlot = inventory.slots[UI_Manager.draggedSlot.slotID];
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventorySlot.itemName);

        if (itemToDrop != null)
        {
            if (UI_Manager.dragSingle)
            {
                GameManager.instance.player.DropItem(itemToDrop);
                inventory.Remove(UI_Manager.draggedSlot.slotID);
            }
            else
            {
                GameManager.instance.player.DropItem(itemToDrop, inventorySlot.count);
                inventory.Remove(UI_Manager.draggedSlot.slotID, inventorySlot.count);
            }

            Refresh();
        }

        UI_Manager.draggedSlot = null;
    }

public void SlotBeginDrag(Slot_UI slot)
{
    UI_Manager.draggedSlot = slot;
    UI_Manager.draggedIcon = Instantiate(slot.itemIcon, slot.itemIcon.transform.position, Quaternion.identity, GameManager.instance.uiManager.transform);
    UI_Manager.draggedIcon.raycastTarget = false;
    UI_Manager.draggedIcon.transform.SetAsLastSibling(); 
}


public void SlotDrag()
{
    if (UI_Manager.draggedIcon != null)
    {
        UI_Manager.draggedIcon.transform.position = Input.mousePosition;
    }
}


public void SlotEndDrag()
{
    if (UI_Manager.draggedIcon != null)
    {
        Destroy(UI_Manager.draggedIcon.gameObject);
        UI_Manager.draggedIcon = null;
    }
    UI_Manager.draggedSlot = null;
}


public void SlotDrop(Slot_UI slot)
{
    // Ensure the dragged slot and inventory are valid
    if (UI_Manager.draggedSlot != null && UI_Manager.draggedSlot.inventory != null)
    {
        Inventory draggedInventory = UI_Manager.draggedSlot.inventory;
        Inventory targetInventory = slot.inventory;

        // Get the slots from both inventories
        Inventory.Slot fromSlot = draggedInventory.slots[UI_Manager.draggedSlot.slotID];
        Inventory.Slot toSlot = targetInventory.slots[slot.slotID];

        // Debug log before moving
        Debug.Log($"Dragging item: {fromSlot.itemReference?.data.itemName} from slot {fromSlot.slotID} to slot {toSlot.slotID}");

        // Transfer the reference
        toSlot.itemReference = fromSlot.itemReference;

        // Move the item
        draggedInventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, targetInventory);

        // Debug log after moving
        Debug.Log($"Dropped item: {toSlot.itemReference?.data.itemName} into slot {toSlot.slotID}");

        // Refresh all UI to reflect changes
        GameManager.instance.uiManager.RefreshAll();
    }
    else
    {
        Debug.LogWarning("Invalid slot drop: Dragged slot or inventory is null.");
    }
}






    private void MoveToMousePosition(GameObject toMove)
    {
        if (canvas != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out Vector2 position);
            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    private void SetupSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].slotID = i;
            slots[i].inventory = inventory;
        }
    }
}
