using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();
    [SerializeField] private InventoryManager inventoryManager;
    


    private Slot_UI selectedSlot;

    private void Start()
    {
        SelectSlot(0);
    }

    private void Update()
    {
        CheckAlphaNumericKeys();
    }

    public void SelectSlot(Slot_UI slot)
    {
        SelectSlot(slot.slotID);
    }



public void SelectSlot(int index)
{
    if (index >= 0 && index < toolbarSlots.Count) // Ensure the index is valid
    {
        if (selectedSlot != null)
        {
            selectedSlot.SetHighlight(false); // Unhighlight the previous slot
        }

        selectedSlot = toolbarSlots[index]; // Update the selected slot
        selectedSlot.SetHighlight(true);    // Highlight the new slot

        // Sync with the inventory manager
        inventoryManager.toolbar.SelectSlot(index);

        // Debug: Log the selected item's details
        Debug.Log($"Selected toolbar slot item: {inventoryManager.toolbar.selectedSlot?.itemName}, " + $"Reference: {inventoryManager.toolbar.selectedSlot?.itemReference?.data.itemName}");
    }
    else
    {
        Debug.LogWarning($"Invalid toolbar slot index: {index}");
    }
}




    private void CheckAlphaNumericKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }
         if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlot(5);
        }
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectSlot(6);
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectSlot(7);
        }
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectSlot(8);
        }
    }
}
