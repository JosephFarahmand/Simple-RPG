using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform target;

    //public Vector3 offset;
    //public float zoomSpeed = 4f;
    //public float maxZoom = 5f;
    //public float minZoom = 15f;

    //public float pitch = 2f;

    //public float yawSpeed = 100f;

    //private float currentZoom = 1f;
    //private float currentYaw = 0f;

    //void Update()
    //{
    //    float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

    //    if (scroll != 0f)
    //    {
    //        currentZoom -= scroll;
    //        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    //    }

    //    var input = -Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

    //    if (input != 0)
    //    {
    //        currentYaw -= input;
    //    }
    //}

    //void LateUpdate()
    //{
    //    transform.position = target.position - offset * currentZoom;
    //    transform.LookAt(target.position + Vector3.up * pitch);

    //    transform.RotateAround(target.position, Vector3.up, currentYaw);
    //}

    [SerializeField] GameObject freeLookCamera;
    CinemachineFreeLook freeLookComponent;

    private void Awake()
    {
        freeLookComponent = freeLookCamera.GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // use the following line for mouse control of zoom instead of mouse wheel
            // be sure to change Input Axis Name on the Y axis to "Mouse Y"

            //freeLookComponent.m_YAxis.m_MaxSpeed = 10;
            freeLookComponent.m_XAxis.m_MaxSpeed = 500;
        }
        if (Input.GetMouseButtonUp(1))
        {
            // use the following line for mouse control of zoom instead of mouse wheel
            // be sure to change Input Axis Name on the Y axis from to "Mouse Y"

            //freeLookComponent.m_YAxis.m_MaxSpeed = 0;
            freeLookComponent.m_XAxis.m_MaxSpeed = 0;
        }

        // wheel zoom //
        // comment out the below if condition if you are using mouse control for zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            freeLookComponent.m_YAxis.m_MaxSpeed = 50;
        }
    }
}