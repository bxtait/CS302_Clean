using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton design pattern to only manage the game once using static reference
    public static GameManager instance;

    public ItemManager itemManager;

    public TileManager tileManager;

    public UI_Manager uiManager;

    public InventoryManager inventoryManager;

    public Player player;



private void Awake()
{
    if(instance != null && instance != this)
    {
        Destroy(this.gameObject);
    }
    else
    {
        instance = this;
    }
    DontDestroyOnLoad(this.gameObject);
    itemManager = GetComponent<ItemManager>();
    tileManager = GetComponent<TileManager>();
    uiManager = GetComponent<UI_Manager>();

    player = FindObjectOfType<Player>();
} 



}
