using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject objectPoolPrefab;
    CameraController cameraController;

    GameObject objectPool;

    private bool initZoomFlag = false;

    private void Awake()
    {
        Instance = this;
        // Ä¿¼­ ¼û±è
        Cursor.visible = false;
        objectPool = Instantiate(objectPoolPrefab);
        objectPool.transform.position = Vector3.zero;
        objectPool.name = "ObjectPool";

    }
    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(Player.Instance.gameObject);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
        }

        InitZoom();
    }

    void InitZoom()
    {
        if (initZoomFlag) return;

        CameraController.Instance.ZoomOut();
        if(CameraController.Instance.Zoom >= CameraController.maxZoom)
        {
            initZoomFlag = true;
        }
    }
}
