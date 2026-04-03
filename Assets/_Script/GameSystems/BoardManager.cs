using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
public class BoardManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Tile> tiles = new();
    private Dictionary<Vector2Int, Perks> perks = new();

    private List<GameObject> coords = new(); //TEMPORARY

    private Vector2Int winTile;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private Material canMoveMaterial;
    [SerializeField] private Material canAttackMaterial;
    [SerializeField] private Material castlingMaterial;
    public bool TileExists(Vector2Int pos)
    {
        if (tiles.ContainsKey(pos))
        {
            return true;
        }

        return false;
    }

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

    public void RegisterCoords(GameObject coord)
    {
        coords.Add(coord);
    }

    public void ClearCoords()
    {
        foreach (var i in new List<GameObject>(coords))
        {
            Destroy(i);
        }
        coords.Clear();
    }

    public void UnregisterTile(Vector2Int pos)
    {
        tiles.Remove(pos);
    }
    public void UnregisterPerk(Vector2Int pos)
    {
        perks.Remove(pos);
    }

    public bool IsWinTile(Vector2Int pos)
    {
        if (winTile != null && winTile == pos)
        {
            return true;
        }
        return false;
    }


    public void CheckPerk(Piece piece, Vector2Int targetPos)
    {
        if (perks.TryGetValue(targetPos, out Perks perk))
        {
            PieceData newData = null;
            bool isEnemy = piece.currentData.isEnemy;
            if (isEnemy)
            {
                newData = perk.GetEnemyPerkPiece();
            }
            else
            {
                newData = perk.GetPlayerPerkPiece();
            }

            piece.Transform(newData);

        }
    }

    public void NotifyPieceEnter(Vector2Int pos, Piece piece)
    {
        if (tiles.TryGetValue(pos, out Tile tile))
        {
            tile.OnPieceEnter(piece);
        }
    }

    public void NotifyPieceExit(Vector2Int pos, Piece piece)
    {
        if (tiles.TryGetValue(pos, out Tile tile))
        {
            tile.OnPieceExit(piece);
        }
    }


    public void ClearBoard()
    {
        foreach (var t in new List<Tile>(tiles.Values))
        {
            if (t != null) Destroy(t.gameObject);
        }
        tiles.Clear();

        foreach (var p in new List<Perks>(perks.Values))
        {
            if (p != null) Destroy(p.gameObject);
        }
        perks.Clear();
    }

    // VISUALS -------------------VISUALS ---------------------------
    public void HighlightTiles(List<Vector2Int> moves, List<Vector2Int> attackMoves)
    {
        if (moves != null)
        {
            foreach (var move in moves)
            {
                if (tiles.TryGetValue(move, out Tile tile))
                {
                    tile.SetMaterial(canMoveMaterial);

                    if (pieceManager.GetPieceAt(move) != null && pieceManager.GetPieceAt(move).currentData == pieceManager._kingData)
                    {
                        tile.SetMaterial(castlingMaterial);

                    }
                }
            }
        }

        if (attackMoves != null)
        {
            foreach (var move in attackMoves)
            {
                if (tiles.TryGetValue(move, out Tile tile))
                {
                    foreach (var enemy in pieceManager.EnemyPieces)
                    {
                        if (enemy.Position == tile.GridPosition)
                        {
                            tile.SetMaterial(canAttackMaterial);
                        }
                    }
                }
            }
        }
    }

    public void ClearHighlights()
    {
        if (tiles.Count == 0) return;

        foreach (var tile in tiles.Values)
        {
            tile.SetMaterial(tile.defaultMaterial);
        }
    }


}
