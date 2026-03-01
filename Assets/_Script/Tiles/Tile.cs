using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
    [field: SerializeField] public Material defaultMaterial { get; private set; }
    [field: SerializeField] public Material activeMaterial { get; private set; }
    public Vector2Int GridPosition { get; private set; }
    [field: SerializeField] public GameObject tileModel { get; private set; }
    [field: SerializeField] public Perks tilePerk { get; private set; }

    [SerializeField] private TileEffect effect;

    public void Initialize(Vector2Int pos)
    {
        GridPosition = pos;
        tileModel.gameObject.GetComponent<Renderer>().material = defaultMaterial;

    }

    public void OnClick()
    {
        GameController.Instance.SelectTile(GridPosition);
    }

    public void OnPieceExit(Piece piece)
    {
        effect?.OnExit(this, piece);
    }

    public void OnPieceEnter(Piece piece)
    {
        effect?.OnEnter(this, piece);
    }

}