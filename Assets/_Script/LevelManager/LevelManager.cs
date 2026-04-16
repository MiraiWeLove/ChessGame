using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TileGenerator tileGenerator;
    [SerializeField] private UIController uiController;
    [SerializeField] private List<SGridMap> levels;
    [SerializeField] private AnimationSystem animationSystem;

    private int currentLevel_number = 0;

    private void Start()
    {
        StartCoroutine(LoadLevel(currentLevel_number));
    }

    public IEnumerator LoadLevel(int index)
    {
        if (index < 0 || index >= levels.Count)
        {
            uiController.ToggleEnd();
            yield break;
        }

        yield return StartCoroutine(GameController.Instance.ClearScene());

        SGridMap loadLevel = levels[index];

        if (loadLevel != null)
        {
            tileGenerator.GenerateLevel(loadLevel);
            currentLevel_number = index;
        }
    }

    public void LoadNextLevel()
    {
        currentLevel_number++;
        StartCoroutine(LoadLevel(currentLevel_number));
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(currentLevel_number));
    }

    public void StartOver()
    {
        currentLevel_number = 0;
        StartCoroutine(LoadLevel(currentLevel_number));
    }

}
