using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private List<PlayerPiece> playerPieces = new();
    private List<EnemyPiece> enemyPieces = new();

    public IReadOnlyList<EnemyPiece> EnemyPieces => enemyPieces;
    public IReadOnlyList<PlayerPiece> PlayerPieces => playerPieces;

    public void RegisterEnemyPiece(EnemyPiece piece)
    {
        enemyPieces.Add(piece);
    }
    public void RegisterPlayerPiece(PlayerPiece piece)
    {
        playerPieces.Add(piece);
    }

    public void UnregisterEnemyPiece(EnemyPiece p)
    {
        enemyPieces.Remove(p);
    }
    public void UnregisterPlayerPiece(PlayerPiece p)
    {
        playerPieces.Remove(p);
    }

    public Piece GetPieceAt(Vector2Int pos)
    {
        foreach (var player in playerPieces)
        {
            if (player.Position == pos)
                return player;
        }

        foreach (var enemy in enemyPieces)
        {
            if (enemy.Position == pos)
                return enemy;
        }

        return null;
    }

    public void RemovePiece(Piece piece)
    {
        if (piece is PlayerPiece player)
            UnregisterPlayerPiece(player);
        else if (piece is EnemyPiece enemy)
            UnregisterEnemyPiece(enemy);

        Destroy(piece.gameObject);
    }

    //public PlayerPiece GetAttackTarget(EnemyPiece enemy)
    //{
    //    var attackMoves = enemy.GetAttackMoves();

    //    foreach (var player in playerPieces)
    //    {
    //        if (attackMoves.Contains(player.Position))
    //        {
    //            return player;
    //        }
    //    }

    //    return null;
    //}


    public void ClearPieces()
    {
        foreach (var p in new List<PlayerPiece>(playerPieces))
        {
            if (p != null) Destroy(p.gameObject);
        }
        playerPieces.Clear();

        foreach (var p in new List<EnemyPiece>(enemyPieces))
        {
            if (p != null) Destroy(p.gameObject);
        }
        enemyPieces.Clear();
    }
}
