using UnityEngine;

public class PawnPerk : Perks
{
    [SerializeField] private PieceData pawnData;

    public override PieceData GetPerk()
    {
        return pawnData;
    }
}
