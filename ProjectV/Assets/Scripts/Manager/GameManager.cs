using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private bool initZoomFlag = false;

    private void Awake()
    {
        Instance = this;
        // Ä¿¼­ ¼û±è
        Cursor.visible = false;
    }
    void Start()
    {
        
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
