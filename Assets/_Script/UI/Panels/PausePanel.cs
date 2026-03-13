using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    public void Restart()
    {
        levelManager.RestartLevel();

        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
