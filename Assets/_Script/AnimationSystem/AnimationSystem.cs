using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    [SerializeField] private AnimationBaseClass _basicMove;

    public bool isAnimating {  get; private set; } = false;

    public void ExecuteAnimation(Transform piece, Vector3 targetPosition, AnimationSystem animationSys)
    {
        _basicMove.PlayAnimation(piece, targetPosition, animationSys);
    }

    public void setAnimating_FALSE ()
    {
        isAnimating = false;
    }
    public void setAnimating_TRUE()
    {
        isAnimating = true;
    }
}
