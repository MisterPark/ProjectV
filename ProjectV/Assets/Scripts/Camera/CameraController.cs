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
    public const float minZoom = 3f;
    public const float maxZoom = 10f;
    [SerializeField] GameObject target;
    [SerializeField] CameraMode mode;
    [SerializeField] Vector3 offset;
    [SerializeField] float zoom;
    [SerializeField] float zoomSpeed = 1.2f;
    [SerializeField] float rotateSpeed = 2.5f;
    private float xRotate = 0f;
    void Start()
    {
        mode = CameraMode.TopView;
    }

    private void Update()
    {
        
    }
    private void LateUpdate()
    {
        Zoom();
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

    private void Zoom()
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
                    offset = new Vector3(-1, 2, 1);
                    transform.position = target.transform.position + offset - (transform.forward * zoom);
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
                    float maxRocateSpeed = 0.4f; // �ִ��ܿ��� ȸ���ӵ���, �������� ������
                    yRotateSize *= 1f - (((zoom - minZoom) * maxRocateSpeed) / (maxZoom - minZoom));
                    //
                    float xRotateSize = -Input.GetAxis("Mouse Y") * rotateSpeed;
                    float yRotate = transform.eulerAngles.y + yRotateSize;

                    // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
                    // Clamp �� ���� ������ �����ϴ� �Լ�
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
        offset += transform.forward * Time.deltaTime;
    }

    public void ZoomOut()
    {
        offset -= transform.forward * Time.deltaTime;
    }

}
