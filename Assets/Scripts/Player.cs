using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private TileManager tileManager;
    private Animator animator;

    private void Start()
    {
        tileManager = GameManager.instance.tileManager; // Reference to TileManager
        inventoryManager = GameManager.instance.inventoryManager; // Reference to InventoryManager
        animator = gameObject.GetComponentInChildren<Animator>(); // Reference to Animator
    }

    private void Update()
    {
        Vector3Int position = tileManager.InteractableMap.WorldToCell(transform.position);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TileData tileData = tileManager.GetTileData(position);

            // Plowing logic
            if (tileData.state == TileState.Default && inventoryManager.toolbar.selectedSlot?.itemName == "Hoe")
            {
                Debug.Log("Plowing tile...");
                tileManager.SetPlowed(position);
            }
            // Planting logic
            else if (tileData.state == TileState.Plowed && inventoryManager.toolbar.selectedSlot != null)
            {
                Item seed = inventoryManager.toolbar.selectedSlot.itemReference;
                tileManager.PlantSeed(position, seed);
                inventoryManager.toolbar.Remove(inventoryManager.toolbar.selectedSlot.slotID, 1);
                Debug.Log("Seed planted.");
            }
            // Advancing growth stages
            else if (tileData.state == TileState.Planted && tileData.growthStage < tileManager.MaxGrowthStages - 1)
            {
                tileManager.AdvanceGrowthStage(position);
                Debug.Log($"Plant advanced to growth stage {tileData.growthStage + 1}.");
            }
            else if (tileData.growthStage == tileManager.MaxGrowthStages - 1)
            {
                Debug.Log("Plant is fully grown.");
            }
        }

        // Harvest logic
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Vector3Int mousePosition = tileManager.InteractableMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            TileData tileData = tileManager.GetTileData(mousePosition);

            if (tileData.state == TileState.Planted && tileData.growthStage == tileManager.MaxGrowthStages - 1)
            {
                Debug.Log($"Harvesting {tileData.seed.data.itemName}.");
                tileManager.HarvestPlant(mousePosition);
            }
        }
    }








    /// <summary>
    /// Drops a single item at the player's position.
    /// </summary>
    /// <param name="item">The item to drop.</param>
    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

        // Instantiate the dropped item in the world
        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);

        // Add force to simulate the drop
        if (droppedItem.rb2d != null) // Ensure Rigidbody2D exists
        {
            droppedItem.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
        }

        Debug.Log($"Dropped item: {item.data.itemName}");
    }

    /// <summary>
    /// Drops multiple items at the player's position.
    /// </summary>
    /// <param name="item">The item to drop.</param>
    /// <param name="numToDrop">The number of items to drop.</param>
    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

}