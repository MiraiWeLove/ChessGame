using UnityEditor.Overlays;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private MoveManager moveManager;

    public void ExecuteEnemyTurn()
    {
        foreach (var enemy in pieceManager.EnemyPieces)
        {
            var target = pieceManager.GetAttackTarget(enemy);

            if (target != null)
            {
                moveManager.ExecuteMove(enemy, target.Position);
                return;
            }
        }
    }
}