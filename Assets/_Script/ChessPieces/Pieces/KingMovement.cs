using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Movements/King")]
public class KingMovement : MovementStrategy
{
    public override List<Vector2Int> GetMoves(Vector2Int position, BoardManager board, PieceManager pieceManager)
    {
        List<Vector2Int> moves = new();

        Vector2Int[] dirs =
        {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down,
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -1),
        };

        foreach (var dir in dirs)
            moves.Add(position + dir);

        return moves;
    }

    public override List<Vector2Int> GetAttackMoves(Vector2Int position, BoardManager board, PieceManager pieceManager)
    {
        return GetMoves(position, board, pieceManager);
    }
}
