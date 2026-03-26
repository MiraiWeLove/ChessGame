using NUnit.Framework;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private BoardManager board;
    [SerializeField] private PieceManager pieceManager;

    public void ExecuteMove(Piece piece, Vector2Int targetPos)
    {
        Vector2Int oldPos = piece.Position;

        Piece pieceOnTarget = pieceManager.GetPieceAt(targetPos);
        if (pieceOnTarget != null)
        {
            pieceManager.RemovePiece(pieceOnTarget);
        }

        board.NotifyPieceExit(oldPos, piece);

        piece.SetPosition(targetPos);
        piece.UpdateViewPosition();

        board.NotifyPieceEnter(targetPos, piece);

        board.ClearHighlights();

        board.CheckPerk(piece, targetPos);
        
        if (board.IsWinTile(targetPos))
        {
            GameController.Instance.DeclareWin();
        }
    }

    public void ExecuteCastling(Vector2Int kingPos, Piece rook)
    {
        Vector2Int rookPos = rook.Position;

        Piece king = pieceManager.GetPieceAt(kingPos);

        Vector2Int dir = rookPos - kingPos;

        dir.x = Mathf.Clamp(dir.x, -1, 1);
        dir.y = Mathf.Clamp(dir.y, -1, 1);

        Vector2Int targetKingPos = kingPos + dir * 2;
        Vector2Int targetRookPos = kingPos + dir;

        ExecuteMove(king, targetKingPos);
        ExecuteMove(rook, targetRookPos);
    }
}