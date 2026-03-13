using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + _cam.transform.forward);
    }

}
