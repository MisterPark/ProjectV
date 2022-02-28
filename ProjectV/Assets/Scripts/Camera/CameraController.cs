using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    Free,
    TopView,
    FirstPerson,
    ThirdPerson,
}

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public const float minZoom = 3f;
    public const float maxZoom = 15f;
    [SerializeField] GameObject target;
    [SerializeField] CameraMode mode;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 topViewOffset;
    [SerializeField] float zoom;
    [SerializeField] float zoomSpeed = 1.2f;
    [SerializeField] float rotateSpeed = 2.5f;
    private float xRotate = 0f;

    public float Zoom { get { return zoom; } }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        mode = CameraMode.TopView;
    }

    private void LateUpdate()
    {
        ProcessZoom();
        FollowTarget();
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetMode(CameraMode mode)
    {
        this.mode = mode;
    }

    public void ShowCursor(bool visible)
    {

    }

    private void ProcessZoom()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float zoom = this.zoom;
        zoom -= wheel;

        zoom = (zoom < minZoom) ? minZoom : zoom;
        zoom = (zoom > maxZoom) ? maxZoom : zoom;

        this.zoom = zoom;
    }

    private void FollowTarget()
    {
        if (target == null) return;

        switch (mode)
        {
            case CameraMode.Free:
                break;
            case CameraMode.TopView:
                {
                    transform.position = target.transform.position + topViewOffset - (transform.forward * zoom);
                    transform.LookAt(target.transform.position);
                    break;
                }
            case CameraMode.FirstPerson:
                break;
            case CameraMode.ThirdPerson:
                {
                    offset = new Vector3(0, 1, 0);
                    float yRotateSize = Input.GetAxis("Mouse X") * rotateSpeed;
                    //
                    float maxRocateSpeed = 0.4f; // 최대줌에서 회전속도율, 작을수록 빠르게
                    yRotateSize *= 1f - (((zoom - minZoom) * maxRocateSpeed) / (maxZoom - minZoom));
                    //
                    float xRotateSize = -Input.GetAxis("Mouse Y") * rotateSpeed;
                    float yRotate = transform.eulerAngles.y + yRotateSize;

                    // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
                    // Clamp 는 값의 범위를 제한하는 함수
                    xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);
                    transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
                    transform.position = target.transform.position + offset - (transform.forward * zoom);
                    break;
                }
            default:
                break;
        }
    }

    public void ZoomIn()
    {
        zoom -= zoomSpeed * Time.deltaTime;
    }

    public void ZoomOut()
    {
        zoom += zoomSpeed * Time.deltaTime;
    }

}
