using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentGameData
{
    public int killCount = 0;
    public int gold = 0;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // ĳ���͵� ������ ���� ���õ� ĳ���� ����
    [ArrayElementTitle("name")]
    public PlayerCharacterNode[] playerCharacterData = new PlayerCharacterNode[(int)PlayerCharacterName.END];
    [HideInInspector] public PlayerCharacterName currentPlayerCharacter = PlayerCharacterName.Character_01;

    // �������� ������ ���ȵ��� �����Ǵ� ��
    public float[] powerUpStat = new float[(int)StatType.END];
    [SerializeField]
    private UI_Powerup_statDB powerStatDB;

    // SaveData ���� ������ �����͵�
    public SaveData currentSaveData;

    // ���� ����ǰ� �ִ� ���� ������
    [SerializeField] public CurrentGameData currentGameData;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SaveDataManager.Instance = GetComponent<SaveDataManager>();
        SaveDataManager.Instance.LoadGameData();
        //SaveDataManager.Instance.SaveGameData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting_PowerStat()
    {
        foreach(Powerup_DataType _powerUp in powerStatDB.Powerup_Type_List)
        {
            powerUpStat[(int)_powerUp.PowerType] = _powerUp.Powerup_Value * _powerUp.Rank;
        }
    }
}
