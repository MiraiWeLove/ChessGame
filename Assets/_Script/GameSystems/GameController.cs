using UnityEngine;

public class GameController : MonoBehaviour
{
    private Piece selectedPiece;
    [SerializeField] private BoardManager board;
    [SerializeField] private MoveManager moveManager;
    public static GameController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectPiece(Piece piece)
    {
        selectedPiece = piece;
        board.HighlightTiles(piece.GetAvailableMoves());
    }
    public void DeselectPiece()
    {
        if (selectedPiece == null) return;

        selectedPiece = null;
        board.ClearHighlights();
    }

    public void SelectTile(Vector2Int pos)
    {
        if (selectedPiece == null) return;

        if (!selectedPiece.GetAvailableMoves().Contains(pos)) return;

        moveManager.ExecuteMove(selectedPiece, pos);
    }
}