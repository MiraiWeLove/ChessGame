using System.Collections;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    public bool isAnimating {  get; private set; } = false;

    public void Play(AnimationType type, Transform piece, Vector3 targetPosition) 
    {
        StartCoroutine(PlayRoutine(type, piece, targetPosition));
    }

    //--------------------------------------
    public void setAnimating_FALSE()
    {
        isAnimating = false;
    }
    public void setAnimating_TRUE()
    {
        isAnimating = true;
    }
    //--------------------------------------


    private IEnumerator PlayRoutine(AnimationType type, Transform piece, Vector3 targetPosition)
    {
        isAnimating = true;

        switch (type)
        {
            case AnimationType.JumpMove:
                yield return StartCoroutine(JumpMove(piece, targetPosition));
                break;
            case AnimationType.TileSpawn:
                yield return StartCoroutine(TileSpawn(piece, targetPosition));
                break;
            case AnimationType.TileDespawn:
                yield return StartCoroutine(TileDespawn(piece, targetPosition));
                break;
            case AnimationType.PieceSpawn:
                yield return StartCoroutine(PieceSpawn(piece, targetPosition));
                break;
        }

        isAnimating = false;
    }

    private IEnumerator PieceSpawn(Transform piece, Vector3 pos)
    {
        setAnimating_TRUE();

        float totalDuration = 0.6f;
        float time = 0f;

        float height = 1f;

        while (time < totalDuration)
        {
            float t = time / totalDuration;

            float yOffset = height * Mathf.Sin(Mathf.PI * t);

            piece.position = new Vector3(pos.x, pos.y + yOffset, pos.z);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = pos;
        setAnimating_FALSE();
    }


    private IEnumerator TileSpawn(Transform piece, Vector3 pos)
    {
        setAnimating_TRUE();

        float totalDuration = 0.3f;
        float time = 0f;

        float height = 0.5f;

        while (time < totalDuration * 0.5f)
        {
            if (piece == null)
                yield break;

            float t = time / (totalDuration * 0.5f);

            float yOffset = height * Mathf.Sin(Mathf.PI * t);

            piece.position = new Vector3(pos.x, pos.y + yOffset, pos.z);

            time += Time.deltaTime;
            yield return null;
        }

        piece.position = pos;
        setAnimating_FALSE();
    }

    private IEnumerator TileDespawn(Transform piece, Vector3 pos)
    {
        setAnimating_TRUE();

        float totalDuration = 0.1f;
        float time = 0f;

        Vector3 startScale = piece.localScale;

        while (time < totalDuration)
        {
            if (piece == null)
                yield break;

            float t = time / totalDuration;

            float scaleFactor = 1f - t;

            piece.localScale = startScale * scaleFactor;

            time += Time.deltaTime;
            yield return null;
        }

        if (piece != null)
            piece.localScale = Vector3.zero;

        setAnimating_FALSE();
    }

    IEnumerator JumpMove(Transform piece, Vector3 targetPosition)
    {
        float durationFactor = 10f;
        float jumpHieghtFactor = 0.4f;
        float turnDegree = -20;
        float turningPointFactor = 0.1f;


        setAnimating_TRUE();

        Vector3 startPosition = piece.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / durationFactor;
        float turningPoint = Mathf.Clamp(duration / turningPointFactor, 0f, 0.5f);

        float direction = (targetPosition.x > startPosition.x) ? -1f : 1f;
        float targetTilt = turnDegree * direction;

        float time = 0f;

        while (time < duration)
        {

            if (piece == null)
                yield break;

            float t = time / duration;

            float rotation;
            if (t <= turningPoint)
            {
                float localT = t / turningPoint;
                rotation = Mathf.Lerp(0f, targetTilt, localT);
            }
            else if (t < 1f - turningPoint)
            {
                rotation = targetTilt;
            }
            else
            {
                float localT = (t - (1f - turningPoint)) / turningPoint;
                rotation = Mathf.Lerp(targetTilt, 0f, localT);
            }

            piece.localEulerAngles = new Vector3(0f, 0f, rotation);

            Vector3 pos = Vector3.Lerp(startPosition, targetPosition, t);
            pos.y += (jumpHieghtFactor * distance) * Mathf.Sin(Mathf.PI * t);

            piece.position = pos;

            time += Time.deltaTime;
            yield return null;
        }

        piece.position = targetPosition;
        piece.localEulerAngles = Vector3.zero;

        setAnimating_FALSE();
    }
}
