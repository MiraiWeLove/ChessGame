using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    public void Restart()
    {
        Debug.Log("RESTART");
        levelManager.RestartLevel();

        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
