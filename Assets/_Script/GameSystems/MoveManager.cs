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

        //CheckEnemyPiece();
        
        if (board.IsWinTile(targetPos))
        {
            DeclareWin();
        }
    }


    //private void CheckEnemyPiece()
    //{

    //    if (pieceManager.EnemyPieces.Count == 0) return;

    //    PlayerPiece pieceToRemove = null;
    //    EnemyPiece enemyPieceToMove = null;
    //    Vector2Int attackPosition = Vector2Int.zero;

    //    foreach (var enemy in pieceManager.EnemyPieces)
    //    {
    //        var moves = enemy.GetAttackMoves();

    //        foreach (var player in pieceManager.PlayerPieces)
    //        {
    //            if (moves.Contains(player.Position))
    //            {
    //                pieceToRemove = player;
    //                enemyPieceToMove = enemy;
    //                attackPosition = player.Position;
    //            }
    //        }
    //    }

    //    if (pieceToRemove != null)
    //    {
    //        pieceManager.RemovePiece(pieceToRemove);
    //        ExecuteMove(enemyPieceToMove, attackPosition);
    //    }
    //}

    private void DeclareWin()
    {
        board.ClearBoard();
        pieceManager.ClearPieces();
        UIController.Instance.HaveWon();
    }
}