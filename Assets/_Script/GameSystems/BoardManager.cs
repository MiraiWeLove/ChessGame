using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
//using System.Linq;
public class BoardManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Tile> tiles = new();
    private Dictionary<Vector2Int, Perks> perks = new();
    private List<GameObject> coords = new(); //TEMPORARY

    private Vector2Int winTile;
    [SerializeField] private PieceManager pieceManager;

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
            PieceData newData = perk.GetPerk();

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
            Destroy(t.gameObject);
        }
        tiles.Clear();

        foreach (var p in new List<Perks>(perks.Values))
        {
            Destroy(p.gameObject);
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
                    bool occupied = pieceManager.PlayerPieces
                        .Any(p => p.Position == tile.GridPosition);

                    if (!occupied)
                    {
                        tile.tileModel.GetComponent<Renderer>().material = tile.activeMaterial;
                    }
                }
            }
        }

        if (attackMoves != null)
        {
            foreach(var move in attackMoves)
            {
                if (tiles.TryGetValue(move, out Tile tile))
                {
                    foreach (var enemy in pieceManager.EnemyPieces)
                    {
                        if (enemy.Position == tile.GridPosition)
                        {
                            tile.tileModel.GetComponent<Renderer>().material = tile.attackMaterial;
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
            tile.tileModel.GetComponent<Renderer>().material = tile.defaultMaterial;
        }
    }


}
