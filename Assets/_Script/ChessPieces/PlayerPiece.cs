using UnityEngine;

public class PlayerPiece : Piece, IClickable
{
    public void OnClick()
    {
        GameController.Instance.DeselectPiece();
        GameController.Instance.SelectPiece(this);
    }
}
