using UnityEngine;

public class DevTools : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private BoardManager boardManager;
    public void ReloadLevel()
    {
        boardManager.ClearCoords();

        levelManager.RestartLevel();
    }
}
