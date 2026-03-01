using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GridMap", menuName = "Scriptables/GridMap")]
public class SGridMap : ScriptableObject
{
    [field: SerializeField] public string GridMapName { get; private set; }
    [field: SerializeField] public List<CellEntry> Cells { get; private set; }

}
