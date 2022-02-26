using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject objectPoolPrefab;
    [SerializeField] GameObject dataManagerPrefab;
    CameraController cameraController;

    GameObject objectPool;
    GameObject dataManager;

    private bool initZoomFlag = false;

    private void Awake()
    {
        Instance = this;
        // Ä¿¼­ ¼û±è
        Cursor.visible = false;
        objectPool = Instantiate(objectPoolPrefab);
        objectPool.transform.position = Vector3.zero;
        objectPool.name = "ObjectPool";

        dataManager = Instantiate(dataManagerPrefab);
        dataManager.transform.position = Vector3.zero;
        dataManager.name = "DataManager";
        //
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
        if(Input.GetKeyUp(KeyCode.F))
        {
            Pause();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            Resume();
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

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
