using System.Collections.Generic;
using System.Linq;
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
        Piece currentPiece = pieceManager.GetPieceAt(position);

        foreach (var offset in dirs)
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
