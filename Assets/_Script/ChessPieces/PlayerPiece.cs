using UnityEngine;

public class PlayerPiece : Piece, IClickable
{
    public void OnClick()
    {
        GameController.Instance.SelectPiece(this);
    }
}
