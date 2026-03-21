using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Piece selectedPiece;
    [SerializeField] private BoardManager board;
    [SerializeField] private MoveManager moveManager;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private PieceManager pieceManager;

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
        board.HighlightTiles(piece.GetAvailableMoves(), piece.GetAttackMoves());
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

        if (!selectedPiece.GetAvailableMoves().Contains(pos) &&
            !selectedPiece.GetAttackMoves().Contains(pos) ||
            (pieceManager.EnemyPieces.Any(p => p.Position == pos) && !selectedPiece.GetAttackMoves().Contains(pos)) ||
            pieceManager.PlayerPieces.Any(p => p.Position == pos)) return;

        moveManager.ExecuteMove(selectedPiece, pos);
        turnManager.ExecuteEnemyTurn();
    }

    public void DeclareWin()
    {
        UIController.Instance.HaveWon();
    }

    public void ClearScene()
    {
        board.ClearBoard();
        pieceManager.ClearPieces();
    }
}