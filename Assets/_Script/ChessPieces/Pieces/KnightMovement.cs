using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Movements/Knight")]
public class KnightMovement : MovementStrategy
{
    public override List<Vector2Int> GetMoves(Vector2Int position, BoardManager board, PieceManager pieceManager)
    {
        List<Vector2Int> moves = new();
        Vector2Int[] offsets =
        {
        new Vector2Int(2, 1), new Vector2Int(2, -1),
        new Vector2Int(-2, 1), new Vector2Int(-2, -1),
        new Vector2Int(1, 2), new Vector2Int(1, -2),
        new Vector2Int(-1, 2), new Vector2Int(-1, -2)
        };

        foreach (var offset in offsets)
        {
            Vector2Int targetPos = position + offset;

            moves.Add(targetPos);
        }

        return moves;
    }

    public override List<Vector2Int> GetAttackMoves(Vector2Int position, BoardManager board, PieceManager pieceManager)
    {
        return GetMoves(position, board, pieceManager);
    }

}
