using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Movements/Pawn")]
public class PawnMovement : MovementStrategy
{
    public override List<Vector2Int> GetMoves(Vector2Int position, BoardManager board, PieceManager pieceManager)
    {
        List<Vector2Int> moves = new();

        Vector2Int[] dirs =
        {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.down,
            Vector2Int.up
        };
        Piece currentPiece = pieceManager.GetPieceAt(position);


        foreach (var offset in dirs)
        {
            Vector2Int newMove = position + offset;

            Piece targetPiece = pieceManager.GetPieceAt(newMove);

            if (targetPiece == null)
            {
                moves.Add(newMove);
            }
        }

        return moves;
    }

    public override List<Vector2Int> GetAttackMoves(Vector2Int position, BoardManager board, PieceManager pieceManager) 
    {
        List<Vector2Int> atMoves = new();

        Vector2Int[] offsets =
        {
        new Vector2Int(1, 1), new Vector2Int(1, -1),
        new Vector2Int(-1, -1), new Vector2Int(-1, 1),
        };

        foreach(var dir in offsets)
        {
            atMoves.Add(position + dir);
        }

        return atMoves;
    }
}
