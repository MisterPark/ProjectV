using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject objectPoolPrefab;
    [SerializeField] GameObject dataManagerPrefab;
    [SerializeField] GameObject boardManagerPrefab;
    [SerializeField] GameObject spawnManagerPrefab;
    [SerializeField] GameObject itemManagerPrefab;
    [SerializeField] GameObject propManagerPrefab;
    [SerializeField] GameObject joystickPrefab;
    CameraController cameraController;

    GameObject player;
    GameObject objectPool;
    GameObject dataManager;
    GameObject boardManager;
    GameObject spawnManager;
    GameObject itemManager;
    GameObject propManager;
    GameObject joystick;

    Joystick _joystick;
    public Joystick Joystick { get { return _joystick; } }

    private bool initZoomFlag = false;

    private void Awake()
    {
        Instance = this;
        // Ä¿¼­ ¼û±è
        HideCursor();
        Random.InitState((int)System.DateTime.UtcNow.Ticks);
#if UNITY_EDITOR || UNITY_ANDROID
        joystick = Instantiate(joystickPrefab);
        joystick.transform.position = Vector3.zero;
        joystick.name = joystickPrefab.name;

        _joystick = joystick.GetComponent<Joystick>();
        GameObject canvas = GameObject.Find("MainGame Canvas");
        if (canvas != null)
        {
            joystick.transform.SetParent(canvas.transform);
        }
#endif

        player = Instantiate(playerPrefab);
        player.transform.position = Vector3.zero;
        player.name = playerPrefab.name;

        objectPool = Instantiate(objectPoolPrefab);
        objectPool.transform.position = Vector3.zero;
        objectPool.name = objectPoolPrefab.name;

        dataManager = Instantiate(dataManagerPrefab);
        dataManager.transform.position = Vector3.zero;
        dataManager.name = dataManagerPrefab.name;
        //
        boardManager = Instantiate(boardManagerPrefab);
        boardManager.transform.position = Vector3.zero;
        boardManager.name = boardManagerPrefab.name;

        spawnManager = Instantiate(spawnManagerPrefab);
        spawnManager.transform.position = Vector3.zero;
        spawnManager.name = spawnManagerPrefab.name;

        itemManager = Instantiate(itemManagerPrefab);
        itemManager.transform.position = Vector3.zero;
        itemManager.name = itemManagerPrefab.name;

        propManager = Instantiate(propManagerPrefab);
        propManager.transform.position = Vector3.zero;
        propManager.name = propManagerPrefab.name;
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

    public void ShowCursor()
    {
#if UNITY_STANDALONE
        Cursor.visible = true;
#endif
    }

    public void HideCursor()
    {
#if UNITY_STANDALONE
        Cursor.visible = false;
#endif
    }
}
