using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    [System.Serializable]
    public class Slot
    {
        public int slotID;
        public string itemName;
        public int count;
        public int maxAllowed;

        public Sprite icon;
        public Item itemReference;

        public Slot(int id)
        {
            slotID = id;
            itemName = "";
            count = 0;
            maxAllowed = 99;
            itemReference = null;
        }
        public bool IsEmpty
        {
            get
            {
                if(itemName == "" && count == 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool CanAddItem(string itemName)
        {
            if(this.itemName == itemName && count < maxAllowed)
            {
                return true;
            }
            return false;
        }
       public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.icon = item.data.icon;
   
            count++;
        }
            public void AddItem(string itemName, Sprite icon, int maxAllowed)
        {
            this.itemName = itemName;
            this.icon = icon;
            count++;
            this.maxAllowed = maxAllowed;
        } 


        public void RemoveItem()
        {
            if(count > 0)
            {
                count--;

                if(count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }
        }
    }
    public List<Slot> slots = new List<Slot>();
    public Slot selectedSlot = null;
    
public Inventory(int numSlots)
{
    for (int i = 0; i < numSlots; i++)
    {
        Slot slot = new Slot(i); // Pass `i` as the ID
        slots.Add(slot);
    }
}


public void Add(Item item)
{
    
    foreach (Slot slot in slots)
    {
        // If the item already exists in the inventory and can be added, do so
        if (slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
        {
            slot.AddItem(item);
            slot.itemReference = item; // Assign the reference to the actual Item
            Debug.Log($"Item {item.data.itemName} assigned to slot {slot.slotID} with reference {slot.itemReference?.data.itemName}");
            return;
        }
    }

    // If no matching slot was found, try to add to an empty slot
    foreach (Slot slot in slots)
    {
        if (slot.IsEmpty)
        {
            slot.AddItem(item);
            slot.itemReference = item; // Assign the reference to the actual Item
            Debug.Log($"Item {item.data.itemName} assigned to slot {slot.slotID} with reference {slot.itemReference?.data.itemName}");
            return;
        }
    }

    Debug.LogWarning($"Could not add item {item.data.itemName}. Inventory is full.");
}


public void Remove(int index)
{
    slots[index].RemoveItem();

}
 // overload the remove function using polymorphism to specify removing multiple items from slots - same function diff parameters
public void Remove(int index, int numToRemove)
{
    if(slots[index].count >= numToRemove)
    {
        for(int i = 0; i < numToRemove; i++)
        {
            Remove(index);
        }
    }

}
public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int numToMove = 1)
{
    Slot fromSlot = slots[fromIndex];
    Slot toSlot = toInventory.slots[toIndex];

    if(toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
    {
        for(int i = 0; i < numToMove; i++)
        {
        toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed);
        fromSlot.RemoveItem();
        }

    }


}

public void SelectSlot(int index)
{
    if (slots != null && slots.Count > 0)
    {
        selectedSlot = slots[index];
    }
}

}

