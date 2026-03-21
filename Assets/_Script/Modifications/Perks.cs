using UnityEngine;

public abstract class Perks : MonoBehaviour
{
    public Vector2Int perkGridPosition { get; private set; }

    public void Initialize(Vector2Int gridPos)
    {
        perkGridPosition = gridPos;
    }

    public abstract PieceData GetPlayerPerkPiece();
    public abstract PieceData GetEnemyPerkPiece();

}
