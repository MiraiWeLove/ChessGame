using UnityEngine;

public struct MoveData
{
    public Piece Piece;
    public Vector2Int TargetPosition;

    public MoveData(Piece piece, Vector2Int target)
    {
        Piece = piece;
        TargetPosition = target;
    }
}