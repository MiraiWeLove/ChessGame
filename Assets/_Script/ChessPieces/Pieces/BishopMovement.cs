using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Movements/Bishop")]

public class BishopMovement : MovementStrategy
{
    private static readonly Vector2Int[] directions =
    {
    new Vector2Int(1, 1),
    new Vector2Int(1, -1),
    new Vector2Int(-1, 1),
    new Vector2Int(-1, -1)
    };

    public override List<Vector2Int> GetMoves(Vector2Int position, BoardManager board, PieceManager pieceManager)
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        foreach (var dir in directions)
        {
            for (int step = 1; step < 24; step++)
            {
                Vector2Int newMove = position + dir * step;

                if (!board.TileExists(newMove)) break;

                moves.Add(newMove);

                Piece targetPiece = pieceManager.GetPieceAt(newMove);

                if (targetPiece != null)
                {
                    moves.Remove(targetPiece.Position);

                    if (pieceManager.GetPieceAt(position) is EnemyPiece && pieceManager.PlayerPieces.Contains(targetPiece))
                    {
                        moves.Add(newMove);
                    }


                    if (pieceManager.GetPieceAt(position) is PlayerPiece && pieceManager.EnemyPieces.Contains(targetPiece))
                    {
                        moves.Add(newMove);
                    }

                    break;
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
