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
}