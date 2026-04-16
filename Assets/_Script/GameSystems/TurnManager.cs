using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private MoveManager moveManager;

    public void ExecuteEnemyTurn()
    {
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
            StartCoroutine(EnemyAttackRoutine(pieceToRemove, enemyPieceToMove, attackPosition));
        }

    }

    IEnumerator EnemyAttackRoutine(Piece target, Piece attacker, Vector2Int pos)
    {
        yield return StartCoroutine(moveManager.ExecuteMove(attacker, pos));

        if (target != null)
            pieceManager.RemovePiece(target);
    }
}