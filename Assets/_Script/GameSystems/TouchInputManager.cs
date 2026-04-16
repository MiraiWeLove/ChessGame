using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputManager : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private GameController gameController;
    [SerializeField] private AnimationSystem animationSystem;

    private void Awake()
    {
        cam = Camera.main;

        if (cam == null)
        {
            cam = FindFirstObjectByType<Camera>();
            Debug.LogWarning("MainCamera not found, using fallback camera.");
        }
    }

    private void Update()
    {
        HandleKeyInputs();

        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(Input.mousePosition);
        }

    }

    private void HandleTouch(Vector2 screenPosition)
    {
        if (animationSystem.isAnimating) return;

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
            UIController.Instance.TogglePause();
        }
    }
}