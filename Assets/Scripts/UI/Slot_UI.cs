using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
    public int slotID; // ID of the slot
    public Inventory inventory; // Reference to the inventory

    public Image itemIcon; // The item icon in the slot
    public TextMeshProUGUI quantityText; // The quantity text

    [SerializeField] private GameObject highlight; // Highlight for selected slots

    // Set an item in this slot
public void SetItem(Inventory.Slot slot)
{
    if (slot != null && !slot.IsEmpty)
    {
        itemIcon.sprite = slot.icon; // Assign the item's icon
        itemIcon.color = new Color(1, 1, 1, 1); // Make the icon visible
        quantityText.text = slot.count > 1 ? slot.count.ToString() : ""; // Show count if > 1

        // Assign itemReference from the slot's itemReference
        if (slot.itemReference != null)
        {
            Debug.Log($"Slot {slot.slotID} assigned item: {slot.itemReference.data.itemName}");
        }
        else
        {
            Debug.LogWarning($"Slot {slot.slotID} has no valid itemReference.");
        }
    }
    else
    {
        SetEmpty(); // Clear slot if no item is assigned
    }
}





    // Clear the slot and set it to empty
public void SetEmpty()
{
    itemIcon.sprite = null; // Clear the icon
    itemIcon.color = new Color(1, 1, 1, 0); // Make the icon invisible
    quantityText.text = ""; // Clear the quantity text
}


    // Toggle the highlight on/off
    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }
}
