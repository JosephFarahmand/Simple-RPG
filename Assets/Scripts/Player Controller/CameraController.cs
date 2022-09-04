using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float maxZoom = 5f;
    public float minZoom = 15f;

    public float pitch = 2f;

    public float yawSpeed = 100f;

    private float currentZoom = 1f;
    private float currentYaw = 0f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (scroll != 0f)
        {
            currentZoom -= scroll;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        var input = Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        if (input != 0)
        {
            currentYaw -= input;
        }
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}