using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    public void HandleNext()
    {
        levelManager.LoadNextLevel();

        this.gameObject.SetActive(false);
    }

    public void HandleRetry()
    {
        levelManager.RestartLevel();

        this.gameObject.SetActive(false);
    }

}
