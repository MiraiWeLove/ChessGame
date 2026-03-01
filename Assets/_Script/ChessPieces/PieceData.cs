using UnityEngine;

[CreateAssetMenu (fileName = "PieceData", menuName = "Scriptables/PieceData")]
public class PieceData : ScriptableObject
{
    [field: SerializeField] public Mesh mesh { get; private set; }
    [field: SerializeField] public Material material { get; private set; }
    [field: SerializeField] public MovementStrategy movementType { get; private set; }
    [field: SerializeField] public bool isEnemy { get; private set; }
}
