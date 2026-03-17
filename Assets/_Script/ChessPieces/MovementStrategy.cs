using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStrategy : ScriptableObject
{
    public abstract List<Vector2Int> GetMoves(Vector2Int position, BoardManager board, PieceManager pieceManager);
    public abstract List<Vector2Int> GetAttackMoves(Vector2Int position, BoardManager board, PieceManager pieceManager);
}
