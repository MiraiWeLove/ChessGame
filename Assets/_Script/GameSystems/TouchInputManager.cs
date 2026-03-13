using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private GameController gameController;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(Input.mousePosition);
        }

        HandleKeyInputs();
#endif

#if UNITY_ANDROID || UNITY_IOS
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            HandleTouch(touch.position);
        }
    }
#endif
    }

    private void HandleTouch(Vector2 screenPosition)
    {
        Ray ray = cam.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IClickable clickable = hit.collider.GetComponent<IClickable>();

            if (clickable != null)
            {
                clickable.OnClick();
            }
            else
            {
                gameController.DeselectPiece();
            }
        }
        else
        {
            gameController.DeselectPiece();
        }
    }

    private void HandleKeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed.");
            UIController.Instance.TogglePause();
        }
    }
}