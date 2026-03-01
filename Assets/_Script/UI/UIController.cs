using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject results;
    public static UIController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }
    }

    public void HaveWon()
    {
        results.SetActive(true);
    }
}
