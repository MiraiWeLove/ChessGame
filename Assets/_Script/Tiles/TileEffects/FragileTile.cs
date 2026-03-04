using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/TileEffects/Fragile")]

public class FragileTile : TileEffect
{
    public override void OnExit(Tile tile, Piece piece)
    {
        tile.Break();
    }
}
