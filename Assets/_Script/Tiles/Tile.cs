using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
    [field: SerializeField] public Material defaultMaterial { get; private set; }

    public Vector2Int GridPosition { get; private set; }
    [field: SerializeField] public GameObject tileModel { get; private set; }
    public Perks tilePerk { get; private set; }

    private TileEffect effect;
    private BoardManager board;
    private Renderer tileRenderer;

    public void Initialize(Vector2Int pos, BoardManager board)
    {
        this.board = board;

        GridPosition = pos;
        tileRenderer = tileModel.gameObject.GetComponent<Renderer>();
        SetMaterial(defaultMaterial);
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

    public void Break()
    {
        board.UnregisterTile(GridPosition);
        Destroy(gameObject);
    }

    public void SetMaterial(Material mat)
    {
        tileRenderer.material = mat;
    }
}