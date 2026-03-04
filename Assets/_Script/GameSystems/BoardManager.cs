using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Tile> tiles = new();
    private Dictionary<Vector2Int, Perks> perks = new();

    private Vector2Int winTile;

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
    public void HighlightTiles(List<Vector2Int> moves)
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
