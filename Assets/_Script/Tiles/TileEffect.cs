using UnityEngine;

public class TileEffect : ScriptableObject
{
    public virtual void OnEnter(Tile tile, Piece piece) { }
    public virtual void OnExit(Tile tile, Piece piece) { }
}
