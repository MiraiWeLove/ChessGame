using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TileGenerator tileGenerator;
    [SerializeField] private List<SGridMap> levels;

    private int currentLevel_number = 0;

    private void Start()
    {
        LoadLevel(currentLevel_number);
    }

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levels.Count)
        {
            Debug.LogWarning("Level index out of range");
            return;
        }

        GameController.Instance.ClearScene();

        SGridMap loadLevel = levels[index];

        if (loadLevel != null)
        {
            tileGenerator.GenerateLevel(loadLevel);
            currentLevel_number = index;
        }
        else
        {
            Debug.LogWarning("Level data is null");
        }
    }

    public void LoadNextLevel()
    {
        currentLevel_number++;
        LoadLevel(currentLevel_number);
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel_number);
    }


}
