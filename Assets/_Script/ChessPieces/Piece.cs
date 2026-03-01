using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Vector2Int Position { get; private set; }

    private PieceData currentData;

    public void Initialize(PieceData data, Vector2Int pos)
    {
        currentData = data;

        Position = pos;
        ApplyVisuals();
    }

    public void Transform(PieceData newData)
    {
        currentData = newData;
        ApplyVisuals();
    }

    private void ApplyVisuals()
    {
        MeshFilter pieceFilter = GetComponent<MeshFilter>();
        MeshRenderer pieceRenderer = GetComponent<MeshRenderer>();

        pieceFilter.mesh = currentData.mesh;
        pieceRenderer.material = currentData.material;
    }

    public List<Vector2Int> GetAvailableMoves()
    {
        return currentData.movementType.GetMoves(Position);
    }

    public List<Vector2Int> GetAttackMoves()
    {
        return currentData.movementType.GetAttackMoves(Position);
    }

    public void SetPosition(Vector2Int pos)
    {
        Position = pos;
    }

    public void UpdateViewPosition()
    {
        transform.position = new Vector3(Position.x, 0, Position.y);
    }
}
