using System.Collections;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private BoardManager board;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private AnimationSystem animationSystem;

    public IEnumerator ExecuteMove(Piece piece, Vector2Int targetPos)
    {

        yield return new WaitUntil(() => !animationSystem.isAnimating);

        Vector2Int oldPos = piece.Position;
        Vector3 vec3TargetPos = new Vector3(targetPos.x, 0, targetPos.y);

        Piece pieceOnTarget = pieceManager.GetPieceAt(targetPos);
        if (pieceOnTarget != null)
        {
            pieceManager.RemovePiece(pieceOnTarget);
        }

        board.NotifyPieceExit(oldPos, piece);

        //animationSystem.ExecuteAnimation(piece.transform, vec3TargetPos, animationSystem);
        animationSystem.Play(AnimationType.JumpMove, piece.transform, vec3TargetPos);
        piece.SetPosition(targetPos);

        board.NotifyPieceEnter(targetPos, piece);

        board.ClearHighlights();

        yield return new WaitUntil(() => !animationSystem.isAnimating);
        board.CheckPerk(piece, targetPos);
        
        if (board.IsWinTile(targetPos))
        {
            GameController.Instance.DeclareWin();
        }
    }

    public IEnumerator ExecuteCastling(Vector2Int kingPos, Piece rook)
    {
        Vector2Int rookPos = rook.Position;

        Piece king = pieceManager.GetPieceAt(kingPos);

        Vector2Int dir = rookPos - kingPos;

        dir.x = Mathf.Clamp(dir.x, -1, 1);
        dir.y = Mathf.Clamp(dir.y, -1, 1);

        Vector2Int targetKingPos = kingPos + dir * 2;
        Vector2Int targetRookPos = kingPos + dir;   

        yield return StartCoroutine(ExecuteMove(king, targetKingPos));

        yield return StartCoroutine(ExecuteMove(rook, targetRookPos));
    }
}