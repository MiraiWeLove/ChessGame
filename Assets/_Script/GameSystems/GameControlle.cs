using System.Collections.Generic;
using UnityEngine;

//THIS WHERE I SPAGHETTI FUCK THE CODE, BUT IT IS TEMPORARY (TO GET THE IDEA CORRECT): SO FUCK OFF!!!! --------------------------------------------------------------
public class GameControll: MonoBehaviour
{
    //public static GameController Instance;

    private Dictionary<Vector2Int, Tile> tiles = new();
    private Dictionary<Vector2Int, Perks> perks = new();
    private List<EnemyPiece> enemyPieces = new();
    private List<PlayerPiece> playerPieces = new();

    //private GameObject selectedPiece;
    private Piece selectedPiece;
    private Vector2Int winTile;


    //private void Awake()
    //{
    //    Instance = this;
    //}

    //REGISTER ALL TILES AND PERKS HERE-------------------
    public void RegisterTile(Tile tile)
    {
        tiles.Add(tile.GridPosition, tile);
    }

    public void RegisterPerk(Perks perk)
    {
        perks.Add(perk.perkGridPosition, perk);
    }

    public void RegisterWinTile(Vector2Int pos)
    {
        winTile = pos;
    }

    public void RegisterEnemyPiece(EnemyPiece enemyPiece)
    {
        enemyPieces.Add(enemyPiece);
    }

    public void RegisterPlayerPiece(PlayerPiece playerPiece)
    {
        playerPieces.Add(playerPiece);
    }

    //unregister.....

    public void UnregisterTile(Vector2Int cellPosition)
    {
        tiles.Remove(cellPosition);
    }
    public void UnregisterPlayerPiece(PlayerPiece piece)
    {
        playerPieces.Remove(piece);
    }
    public void UnregisterEnemyPiece(EnemyPiece piece)
    {
        enemyPieces.Remove(piece);
    }
    //------------------------------------------------

    public void SelectPiece(Piece piece)
    {
        selectedPiece = piece;
        var moves = piece.GetAvailableMoves();
        HighlightTiles(moves);
    }

    public void DeselectPiece()
    {
        selectedPiece = null;
        ClearHighlights();
    }

    public void SelectTile(Vector2Int pos)
    {

        if (selectedPiece == null)
        {
            return;
        }

        var validMoves = selectedPiece.GetAvailableMoves();

        if (!validMoves.Contains(pos))
        {
            return;
        }

        MovePiece(selectedPiece, pos);
    }

    private void MovePiece(Piece piece, Vector2Int pos)
    {
        Piece enemyOnTile = CheckForPiece(pos);

        if (enemyOnTile != null) Destroy(enemyOnTile.gameObject);

        Vector2Int oldPos = piece.Position;

        if (tiles.TryGetValue(oldPos, out Tile oldTile))
        {
            oldTile.OnPieceExit(piece);
        }

        piece.SetPosition(pos);
        piece.UpdateViewPosition();

        if (tiles.TryGetValue(pos, out Tile newTile))
        {
            newTile.OnPieceEnter(piece);
        }

        ClearHighlights();

        CheckPerk(selectedPiece, pos);

        selectedPiece = null;

        if (IsWin(pos))
            UIController.Instance.HaveWon();

        CheckEnemyPiece();
    }

    private bool IsWin(Vector2Int pos)
    {
        if (winTile != null)
        {
            if (winTile == pos)
            {
                return true;
            }
        }
        return false;
    }

    private Piece CheckForPiece(Vector2Int targetPos)
    {
        foreach (var piece in enemyPieces)
        {
            if (targetPos == piece.Position)
            {
                UnregisterEnemyPiece(piece);
                return piece;
            }
        }

        return null;
    }

    private void CheckPerk(Piece piece, Vector2Int targetPos)
    {
        if (perks.TryGetValue(targetPos, out Perks perk))
        {
            if (selectedPiece != null)
            {

                PieceData newData = perk.GetPerk();

                piece.Transform(newData);
            }
        }
    }

    private void CheckEnemyPiece()
    {
        if (enemyPieces.Count == 0) return;

        PlayerPiece pieceToRemove = null;
        EnemyPiece enemyPieceToMove = null;
        Vector2Int attackPosition = Vector2Int.zero;

        foreach (var enemy in enemyPieces)
        {
            var moves = enemy.GetAttackMoves();

            foreach (var player in playerPieces)
            {
                if (moves.Contains(player.Position))
                {
                    Destroy(player.gameObject);
                    UnregisterPlayerPiece(player);
                    MovePiece(enemy, player.Position);
                    return;
                }
            }
        }

        if (pieceToRemove != null)
        {
            Destroy(pieceToRemove.gameObject);
            UnregisterPlayerPiece(pieceToRemove);

            MovePiece(enemyPieceToMove, attackPosition);
        }
    }

    //VISUALS-------------------------------------
    private void HighlightTiles(List<Vector2Int> moves)
    {
        if (moves != null)
        {
            foreach (var move in moves)
            {
                if (tiles.TryGetValue(move, out Tile tile))
                {
                    tile.tileModel.GetComponent<Renderer>().material = tile.activeMaterial;
                }
            }
        }

    }

    public void ClearHighlights()
    {
        if (tiles.Count == 0) return;

        foreach (var tile in tiles.Values)
        {
            tile.tileModel.GetComponent<Renderer>().material = tile.defaultMaterial;
        }
    }
}