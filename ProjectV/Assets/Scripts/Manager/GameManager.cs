using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourEx
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
    [SerializeField] GameObject combineSkillManagerPrefab;
    [SerializeField] GameObject soundManagerPrefab;
    CameraController cameraController;

    GameObject player;
    GameObject objectPool;
    GameObject dataManager;
    GameObject boardManager;
    GameObject spawnManager;
    GameObject itemManager;
    GameObject propManager;
    GameObject joystick;
    GameObject combineSkillManager;
    GameObject soundManager;

    Joystick _joystick;
    public Joystick Joystick { get { return _joystick; } }

    private bool initZoomFlag = false;
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
#if UNITY_EDITOR
        Application.targetFrameRate = 50;
#elif UNITY_ANDROID
        // fps °íÁ¤
        Application.targetFrameRate = 50;
#endif
        // Ä¿¼­ ¼û±è
        HideCursor();
        Random.InitState((int)System.DateTime.UtcNow.Ticks);

        joystick = Instantiate(joystickPrefab);
        joystick.name = joystickPrefab.name;

        _joystick = joystick.GetComponent<Joystick>();

        GameObject canvas = GameObject.Find("MainGame Canvas");
        if (canvas != null)
        {
            joystick.transform.SetParent(canvas.transform);
            joystick.transform.SetAsFirstSibling();
        }

        //player = Instantiate(playerPrefab);
        //player = Instantiate(Resources.Load("Unit/Player/Player")) as GameObject;
        //player.transform.position = Vector3.zero;
        //player.name = playerPrefab.name;

        if (SoundManager.Instance == null)
        {
            soundManager = Instantiate(soundManagerPrefab);
            soundManager.transform.position = Vector3.zero;
            soundManager.name = soundManagerPrefab.name;
        }

        objectPool = Instantiate(objectPoolPrefab);
        objectPool.transform.position = Vector3.zero;
        objectPool.name = objectPoolPrefab.name;

        if (DataManager.Instance == null)
        {
            dataManager = Instantiate(dataManagerPrefab);
            dataManager.transform.position = Vector3.zero;
            dataManager.name = dataManagerPrefab.name;
        }


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

        combineSkillManager = Instantiate(combineSkillManagerPrefab);
        combineSkillManager.transform.position = Vector3.zero;
        combineSkillManager.name = combineSkillManagerPrefab.name;

        
    }
    protected override void Start()
    {
        base.Start();
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(Player.Instance.gameObject);
    }

    public override void FixedUpdateEx()
    {
        InitZoom();
        CountTime();
        ProcessVictory();
    }

    public override void UpdateEx()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            UI_PausePanel.instance.Show(!UI_PausePanel.instance.Visible);
        }
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.F))
        {
            Pause();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            Resume();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            UI_Gameover.instance.Show();
        }
        if (Input.GetKeyUp(KeyCode.F1))
        {
            DataManager.Instance.currentGameData.totalPlayTime += 60f;
        }
        if (Input.GetKeyUp(KeyCode.F2))
        {
            float exp = Player.Instance.stat.Get_FinalStat(StatType.MaxExp);
            Player.Instance.stat.Increase_FinalStat(StatType.Exp, exp);
        }
        if (Input.GetKeyUp(KeyCode.F3))
        {
            Player.Instance.stat.Increase_FinalStat(StatType.MoveSpeed, 100);
        }
        if (Input.GetKeyUp(KeyCode.F4))
        {
            SpawnManager.Instance.Pause = !SpawnManager.Instance.Pause;
        }
        if (Input.GetKeyUp(KeyCode.F5))
        {
            BoardManager.Instance.Pause = !BoardManager.Instance.Pause;
        }
        if (Input.GetKeyUp(KeyCode.F6))
        {
            float hp = Player.Instance.stat.Get_FinalStat(StatType.MaxHealth);
            Player.Instance.stat.Set_FinalStat(StatType.Health, hp);
        }
        if (Input.GetKeyUp(KeyCode.F7))
        {
            float timescale = Time.timeScale;
            timescale = timescale > 0f ? 1f : 0f;
            Time.timeScale = timescale;
        }
#endif

        

    }

    void InitZoom()
    {
        if (initZoomFlag) return;

        CameraController.Instance.ZoomOut();
        if (CameraController.Instance.Zoom >= CameraController.maxZoom)
        {
            initZoomFlag = true;
        }
    }

    void CountTime()
    {
        DataManager.Instance.currentGameData.totalPlayTime += Time.fixedDeltaTime;
    }

    void ProcessVictory()
    {
        if (DataManager.Instance.currentGameData.totalPlayTime >= 1800f)
        {
            Pause();
            UI_Victory.instance.Show();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if (_joystick != null)
        {
            _joystick.PointerUp();
            _joystick.Hide();
            _joystick.gameObject.SetActive(false);
        }

        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        if (_joystick != null)
        {
            _joystick.gameObject.SetActive(true);
            //_joystick.Show();
        }
        isPaused = false;
    }

    public void ShowCursor()
    {
#if UNITY_STANDALONE
        //Cursor.visible = true;
#endif
    }

    public void HideCursor()
    {
#if UNITY_STANDALONE
        //Cursor.visible = false;
#endif
    }
}
