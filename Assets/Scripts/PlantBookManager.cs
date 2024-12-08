using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBookManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> frames; // Frames in the Plant Book
    private HashSet<string> unlockedPlants = new HashSet<string>(); // Track harvested plants
    


    // Adds a plant to the book
public void AddPlantToBook(ItemData plantData)
{
    
    if (unlockedPlants.Contains(plantData.itemName))
    {
        Debug.Log($"{plantData.itemName} is already in the Plant Book.");
        return;
    }

    // Add this plant to the unlocked plants set
    unlockedPlants.Add(plantData.itemName);

    // Finds the first available frame in the Plant Book
    foreach (var frame in frames)
    {
        // Gets child Image component (the placeholder for the plant icon)
        Image plantIcon = frame.transform.GetChild(0).GetComponent<Image>();

        // If the frame is empty (inactive)
        if (!plantIcon.gameObject.activeSelf)
        {
            // Assign the harvested plant's icon to this frame
            plantIcon.sprite = plantData.icon; 
            plantIcon.gameObject.SetActive(true); // Make the icon visible
            Debug.Log($"{plantData.itemName} added to Plant Book!");
            return; // Exit the loop after adding the plant
        }
    }

    // If all frames are full, log a warning
    Debug.LogWarning("No empty frames available in the Plant Book!");
}

}
