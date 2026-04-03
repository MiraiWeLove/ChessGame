using System.Collections.Generic;
using System.Linq;
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

        Piece currentPiece = pieceManager.GetPieceAt(position);

        foreach (var offset in offsets)
        {
            Vector2Int newMove = position + offset;

            if (!board.TileExists(newMove))
                continue;

            Piece targetPiece = pieceManager.GetPieceAt(newMove);

            if (targetPiece == null)
            {
                moves.Add(newMove);
            }
            else
            {
                // If enemy → can capture
                if (currentPiece is PlayerPiece && pieceManager.EnemyPieces.Contains(targetPiece) ||
                    currentPiece is EnemyPiece && pieceManager.PlayerPieces.Contains(targetPiece))
                {
                    moves.Add(newMove);
                }
            }
        }

        return moves;
    }

    public override List<Vector2Int> GetAttackMoves(Vector2Int position, BoardManager board, PieceManager pieceManager)
    {
        return GetMoves(position, board, pieceManager);
    }

}
