using System.Linq;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    private Piece selectedPiece;
    [SerializeField] private BoardManager board;
    [SerializeField] private MoveManager moveManager;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private AnimationSystem animationSystem;

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
        StartCoroutine(SelectTileRoutine(pos));
    }

    private IEnumerator SelectTileRoutine(Vector2Int pos)
    {

        if (selectedPiece == null)
            yield break;

        bool isMove = selectedPiece.GetAvailableMoves().Contains(pos);
        bool isAttack = selectedPiece.GetAttackMoves().Contains(pos);
        bool hasEnemy = pieceManager.EnemyPieces.Any(p => p.Position == pos);

        if (!isMove && !(isAttack && hasEnemy))
            yield break;

        foreach (var p in pieceManager.PlayerPieces)
        {
            if (p.Position == pos && p.currentData == pieceManager._kingData)
            {
                StartCoroutine(moveManager.ExecuteCastling(pos, selectedPiece));
                yield return new WaitUntil(() => !animationSystem.isAnimating);

                turnManager.ExecuteEnemyTurn();
                yield break;
            }
        }

        if (pieceManager.PlayerPieces.Any(p => p.Position == pos))
            yield break;

        yield return StartCoroutine(moveManager.ExecuteMove(selectedPiece, pos));
        DeselectPiece();
        turnManager.ExecuteEnemyTurn();
    }


    public void DeclareWin()
    {
        UIController.Instance.HaveWon();
    }

    public IEnumerator ClearScene()
    {
        pieceManager.ClearPieces();
        yield return StartCoroutine(board.ClearBoard());
    }
}