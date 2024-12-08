using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName; // Name of the item
    public Sprite icon;     // Icon to display for the item
    public string description; // Description of the item
    public ItemData harvestedItemData; // Reference to the harvested item

    [Range(1, 99)]
    public int maxStackSize = 99; // Maximum number of items in a stack
}
