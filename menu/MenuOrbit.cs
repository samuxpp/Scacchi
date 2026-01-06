using UnityEngine;

public class MenuOrbit : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public bool clockwise = true;
    public bool orbit = true;

    void Update()
    {
        if (orbit)
        {
            float direction = clockwise ? 1f : -1f;
            transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}