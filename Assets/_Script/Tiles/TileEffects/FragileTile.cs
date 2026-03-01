using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/TileEffects/Fragile")]

public class FragileTile : TileEffect
{
    public override void OnExit(Tile tile, Piece piece)
    {
        GameController.Instance.UnregisterTile(tile.GridPosition);

        Destroy(tile.gameObject);
    }
}
