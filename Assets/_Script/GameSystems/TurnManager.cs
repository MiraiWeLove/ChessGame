using UnityEditor.Overlays;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private MoveManager moveManager;

    public void ExecuteEnemyTurn()
    {
        //foreach (var enemy in pieceManager.EnemyPieces)
        //{
        //    var target = pieceManager.GetAttackTarget(enemy);

        //    if (target != null)
        //    {
        //        moveManager.ExecuteMove(enemy, target.Position);
        //        return;
        //    }
        //}

            if (pieceManager.EnemyPieces.Count == 0) return;

        PlayerPiece pieceToRemove = null;
        EnemyPiece enemyPieceToMove = null;
        Vector2Int attackPosition = Vector2Int.zero;

        foreach (var enemy in pieceManager.EnemyPieces)
        {
            var moves = enemy.GetAttackMoves();

            foreach (var player in pieceManager.PlayerPieces)
            {
                if (moves.Contains(player.Position))
                {
                    pieceToRemove = player;
                    enemyPieceToMove = enemy;
                    attackPosition = player.Position;
                }
            }
        }

        if (pieceToRemove != null)
        {
            pieceManager.RemovePiece(pieceToRemove);
            moveManager.ExecuteMove(enemyPieceToMove, attackPosition);
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


}