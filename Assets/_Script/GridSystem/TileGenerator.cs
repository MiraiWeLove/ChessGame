using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Transform tileParent;
    [SerializeField] private SGridMap gridMap; //TEMPORARY----

    [Space]
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject piecePrefab;

    private void Start()
    {
        HandleCreate();
    }

    private void HandleCreate()
    {
        foreach (var c in gridMap.Cells)
        {
            Vector3 cellPos = new Vector3(c.cellPosition.x, 0, c.cellPosition.y);

            GameObject tileObj = Instantiate(
                c.cellPrefab,
                cellPos,
                Quaternion.identity,
                tileParent
            );

            Tile view = tileObj.GetComponent<Tile>();
            view.Initialize(c.cellPosition);

            GameController.Instance.RegisterTile(view);

            if (c.cellPrefab == finishPrefab) GameController.Instance.RegisterWinTile(c.cellPosition);

            if (c.perkPrefab != null)
            {
                GameObject perkObj = Instantiate(
                        c.perkPrefab,
                        cellPos,
                        Quaternion.identity,
                        tileObj.transform
                    );

                Perks perkScript = perkObj.GetComponent<Perks>();
                perkScript.Initialize(c.cellPosition);

                GameController.Instance.RegisterPerk(perkScript);
            }

            if (c.pieceData != null)
            {
                GameObject pieceObj = Instantiate(
                    piecePrefab,
                    cellPos,
                    Quaternion.identity
                    );

                if (c.pieceData.isEnemy)
                {
                    EnemyPiece script = pieceObj.AddComponent<EnemyPiece>();
                    GameController.Instance.RegisterEnemyPiece(script);
                }
                else
                {
                    PlayerPiece script = pieceObj.AddComponent<PlayerPiece>();
                    GameController.Instance.RegisterPlayerPiece(script);
                }

                Piece pieceScript = pieceObj.GetComponent<Piece>();
                pieceScript.Initialize(c.pieceData, c.cellPosition);
            }

        }
    }
}
