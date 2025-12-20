using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
    private float rotationSpeed = 0.2f;
    private Vector2 lastMousePos;
    private float zoomSpeed = 5f;
    private float smoothness = 5f;
    private float minDistance = 3f;
    private float maxDistance = 25f;
    private float targetDistance;

    void Start()
    {
        targetDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    void Update()
    {
        if (Mouse.current.middleButton.isPressed)
        {
            Vector2 currentMousePos = Mouse.current.position.ReadValue();
            Vector2 delta = currentMousePos - lastMousePos;
            transform.Rotate(Vector3.up, delta.x * rotationSpeed, Space.World);
            transform.Rotate(Vector3.right, -delta.y * rotationSpeed, Space.Self);
            LimitRotation();
        }
        lastMousePos = Mouse.current.position.ReadValue();
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll != 0)
        {
            targetDistance -= (scroll / 120f) * zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        }
        Vector3 direzione = (Camera.main.transform.position - transform.position).normalized;
        Vector3 targetPosition = transform.position + (direzione * targetDistance);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, Time.deltaTime * smoothness);
        Camera.main.transform.LookAt(transform.position);
    }
    void LimitRotation()
    {
        Vector3 rot = transform.localEulerAngles;
        float angle = rot.x;
        if (angle > 180) angle -= 360;
        angle = Mathf.Clamp(angle, -30f, 80f);
        transform.localEulerAngles = new Vector3(angle, rot.y, rot.z);
    }
}