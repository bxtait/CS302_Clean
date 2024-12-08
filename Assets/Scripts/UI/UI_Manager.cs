using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour

{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();
    public List<Inventory_UI> inventoryUIs;

    public GameObject inventoryPanel;

    public static Slot_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;
    public GameObject plantBookPanel;
    public PlantBookManager plantBookManager;

    private void Awake()
    {
        Initialize();
        inventoryPanel.SetActive(false); 

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }



         if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }

    public void ToggleInventoryUI()
    {
        if(inventoryPanel != null)
        {
            if(!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            RefreshInventoryUI("Backpack");
    
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
        }
    }
    
    public void RefreshInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }
    
    public void RefreshAll()
    {
        foreach(KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }

    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }
        Debug.LogWarning("there's not inventory UI for " + inventoryName);
        return null;

    }
    void Initialize()
    {
        foreach(Inventory_UI ui in inventoryUIs)
        {
        if(!inventoryUIByName.ContainsKey(ui.inventoryName))
        {
            inventoryUIByName.Add(ui.inventoryName, ui);
        }
        }
    }
        public void ShowMenuUI()
    {
        Debug.Log("Menu is shown");
        SceneManager.LoadScene("MainMenuScene");
    }

      public void AddToPlantBook(Item seed)
    {
        Debug.Log($"{seed.data.itemName} added to Plant Book.");
        
    }
        public void TogglePlantBook()
    {
        if (plantBookPanel != null)
        {
            bool isActive = plantBookPanel.activeSelf; // Checks if it's active
            plantBookPanel.SetActive(!isActive);      // Turns it on/off
            Debug.Log("Plant Book toggled: " + (!isActive ? "Opened" : "Closed"));
        }
        else
        {
            Debug.LogWarning("Plant Book Panel is not assigned in the UI_Manager.");
        }
    }


}
