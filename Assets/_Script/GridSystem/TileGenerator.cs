using TMPro;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Transform objectParent;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PieceManager pieceManager;

    [Space]
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject coordCanvas; //TEMPORARY
    [SerializeField] private bool devTools; //TEMPORARY

    public void GenerateLevel(SGridMap levelData)
    {
        foreach (var c in levelData.Cells)
        {
            Vector3 cellPos = new Vector3(c.cellPosition.x, 0, c.cellPosition.y);

            GameObject tileObj = Instantiate(
                c.cellPrefab,
                cellPos,
                Quaternion.identity,
                objectParent
            );

            if (devTools) 
            {
                CreateCoords(cellPos);
            }

            Tile view = tileObj.GetComponent<Tile>();
            view.Initialize(c.cellPosition, boardManager);

            boardManager.RegisterTile(view);

            if (c.cellPrefab == finishPrefab) boardManager.RegisterWinTile(c.cellPosition);

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

                boardManager.RegisterPerk(perkScript);
            }

            if (c.pieceData != null)
            {
                GameObject pieceObj = Instantiate(
                    piecePrefab,
                    cellPos,
                    Quaternion.identity,
                    objectParent
                    );

                if (c.pieceData.isEnemy)
                {
                    EnemyPiece script = pieceObj.AddComponent<EnemyPiece>();
                    pieceManager.RegisterEnemyPiece(script);
                }
                else
                {
                    PlayerPiece script = pieceObj.AddComponent<PlayerPiece>();
                    pieceManager.RegisterPlayerPiece(script);
                }

                Piece pieceScript = pieceObj.GetComponent<Piece>();
                pieceScript.Initialize(c.pieceData, c.cellPosition);
            }
        }
    }

    private void CreateCoords(Vector3 pos)
    {
        Vector3 newPos = new Vector3(pos.x, pos.y + 0.2f, pos.z);

        GameObject coord = Instantiate
            (
            coordCanvas,
            newPos,
            Quaternion.identity
            );

        boardManager.RegisterCoords(coord);
    }
}