using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Movements/Rook")]
public class RookMovement : MovementStrategy
{
    public bool canCastle = true;

    private static readonly Vector2Int[] directions =
    {
    Vector2Int.right,Vector2Int.left,
    Vector2Int.up, Vector2Int.down,
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
                    if (targetPiece.currentData == pieceManager._kingData) 
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
