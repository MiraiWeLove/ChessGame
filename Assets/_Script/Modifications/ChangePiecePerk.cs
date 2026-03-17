using UnityEngine;

public class ChangePiecePerk : Perks
{
    [SerializeField] private PieceData pawnData;

    public override PieceData GetPerk()
    {
        return pawnData;
    }
}
