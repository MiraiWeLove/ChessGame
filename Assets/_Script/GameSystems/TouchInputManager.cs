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
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(Input.mousePosition);
        }

        HandleKeyInputs();

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
            UIController.Instance.TogglePause();
        }
    }
}