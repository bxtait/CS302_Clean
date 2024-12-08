using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap interactableMap; // Reference to the Tilemap
    [SerializeField] private Sprite plowedTileSprite; // Sprite for plowed tiles
    [SerializeField] private List<Sprite> growthSprites; // List of growth stage sprites

    public Tilemap InteractableMap => interactableMap; // Getter for the interactable map
    public int MaxGrowthStages => growthSprites.Count; // Maximum number of growth stages

    private Dictionary<Vector3Int, TileData> tileDataDictionary = new Dictionary<Vector3Int, TileData>();

    public TileData GetTileData(Vector3Int position)
    {
        if (!tileDataDictionary.ContainsKey(position))
        {
            tileDataDictionary[position] = new TileData(); // Initialize default tile data
            Debug.Log($"TileData initialized for position {position}.");
        }
        return tileDataDictionary[position];
    }

    public bool IsTileInInteractableMap(Vector3Int position)
    {
        return interactableMap.HasTile(position); // Check if the position has a tile in the interactable map
    }

    public void SetPlowed(Vector3Int position)
    {
        if (!IsTileInInteractableMap(position))
        {
            Debug.LogWarning($"Tile at {position} is not part of the interactable map. Cannot plow.");
            return;
        }

        TileData tile = GetTileData(position);
        tile.state = TileState.Plowed; // Update state
        UpdateTileVisual(position, plowedTileSprite); // Update visual
        Debug.Log($"Tile at {position} plowed successfully.");
    }

    public void PlantSeed(Vector3Int position, Item seed)
    {
        TileData tileData = GetTileData(position);

        if (tileData.state == TileState.Plowed) // Only allow planting on plowed tiles
        {
            tileData.state = TileState.Planted;
            tileData.seed = seed;
            tileData.growthStage = 0;
            UpdateTileVisual(position, growthSprites[0]); // Use the first growth stage sprite
            Debug.Log($"Seed {seed.data.itemName} planted at {position}.");
        }
        else
        {
            Debug.LogWarning("Cannot plant seed. Tile is not plowed.");
        }
    }

    public void AdvanceGrowthStage(Vector3Int position)
    {
        TileData tileData = GetTileData(position);

        if (tileData.state == TileState.Planted && tileData.growthStage < MaxGrowthStages - 1)
        {
            tileData.growthStage++;
            UpdateTileVisual(position, growthSprites[tileData.growthStage]); // Update the sprite to the next stage
            Debug.Log($"Tile at {position} advanced to growth stage {tileData.growthStage}.");
        }
    }

public void HarvestPlant(Vector3Int position)
{
    TileData tileData = GetTileData(position);

    if (tileData.state == TileState.Planted && tileData.growthStage == MaxGrowthStages - 1)
    {
        Debug.Log($"Harvesting {tileData.seed.data.itemName} at {position}.");
        GameManager.instance.uiManager.plantBookManager.AddPlantToBook(tileData.seed.data);


        // Check if a harvested item is defined
        ItemData harvestedData = tileData.seed.data.harvestedItemData;
        if (harvestedData != null)
        {
            // Create a new Item for the harvested flower
            Item harvestedItem = new Item
            {
                data = harvestedData // Assign the harvested item data
            };

            // Add the harvested item to the inventory
            GameManager.instance.inventoryManager.Add("Backpack", harvestedItem);
            Debug.Log($"Added {harvestedData.itemName} to inventory.");
        }
        else
        {
            Debug.LogWarning("No harvested item defined. Defaulting to seed item.");
            GameManager.instance.inventoryManager.Add("Backpack", tileData.seed);
        }

        // Reset tile data
        tileData.state = TileState.Default;
        tileData.seed = null;
        tileData.growthStage = 0;

        UpdateTileVisual(position, null); // Clear the visual
    }
}


    private void UpdateTileVisual(Vector3Int position, Sprite newSprite)
    {
        if (interactableMap != null)
        {
            if (newSprite != null)
            {
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = newSprite;
                interactableMap.SetTile(position, tile); // Update the tile with the new sprite
            }
            else
            {
                interactableMap.SetTile(position, null); // Clear the tile if no sprite
            }
        }
    }
}
