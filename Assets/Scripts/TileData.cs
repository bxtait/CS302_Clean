using UnityEngine;

public enum TileState
{
    Default,
    Plowed,
    Planted
}

public class TileData
{
    public TileState state = TileState.Default;
    public Item seed;
    public int growthStage = 0;
}


