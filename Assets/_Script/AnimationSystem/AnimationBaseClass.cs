using UnityEngine;

public abstract class AnimationBaseClass : MonoBehaviour
{
    public abstract void PlayAnimation(Transform piece, Vector3 targetPosition, AnimationSystem animationSys);
}
