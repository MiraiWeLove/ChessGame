using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    public void Restart()
    {
        levelManager.RestartLevel();

        gameObject.SetActive(false);
    }

    public void StartOver()
    {
        levelManager.StartOver();
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
