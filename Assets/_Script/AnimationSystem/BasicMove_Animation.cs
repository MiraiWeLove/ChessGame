using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class BasicMove_Animation : AnimationBaseClass
{
    [SerializeField] private float durationFactor = 10f;
    [SerializeField] private float jumpHieghtFactor = 0.4f;
    [SerializeField] private float turnDegree = -20;
    [SerializeField] private float turningPointFactor = 0.1f;

    public override void PlayAnimation(Transform piece, Vector3 targetPosition, AnimationSystem animationSys)
    {
        StartCoroutine(JumpAnim(piece, targetPosition, animationSys));
    }

    IEnumerator JumpAnim(Transform piece, Vector3 targetPosition, AnimationSystem animationSys)
    {
        animationSys.setAnimating_TRUE();

        Vector3 startPosition = piece.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / durationFactor;
        float turningPoint = Mathf.Clamp(duration / turningPointFactor, 0f, 0.5f);

        float direction = (targetPosition.x > startPosition.x) ? -1f : 1f;
        float targetTilt = turnDegree * direction;

        float time = 0f;

        while (time < duration)
        {
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

        animationSys.setAnimating_FALSE();
    }
}