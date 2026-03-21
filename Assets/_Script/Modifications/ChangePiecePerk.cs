using UnityEngine;

public class ChangePiecePerk : Perks
{
    [SerializeField] private PieceData playerPieceData;
    [SerializeField] private PieceData enemyPieceData;

    public override PieceData GetPlayerPerkPiece()
    {
        return playerPieceData;
    }

    public override PieceData GetEnemyPerkPiece()
    {
        return enemyPieceData;
    }
}
