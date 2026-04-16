using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject results;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject theEnd;

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

    public void TogglePause()
    {
        bool pauseStatus = pause.activeSelf;
        if (results.activeSelf) results.SetActive(false);
        pause.SetActive(!pauseStatus);
    }

    public void ToggleEnd()
    {
        bool isActive = theEnd.activeSelf;
        theEnd.SetActive(!isActive);
    }
}
