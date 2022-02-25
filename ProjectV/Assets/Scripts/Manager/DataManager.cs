using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // 캐릭터들 정보와 현재 선택된 캐릭터 정보
    [ArrayElementTitle("name")]
    public PlayerCharacterNode[] playerCharacterData = new PlayerCharacterNode[(int)PlayerCharacterName.END];
    [HideInInspector] public PlayerCharacterName currentPlayerCharacter = PlayerCharacterName.Character_01;

    // 상점에서 구입한 스탯들이 보관되는 곳
    public float[] powerUpStat = new float[(int)StatType.END];

    // SaveData 에서 가져온 데이터들
    public SaveData currentSaveData;

    [SerializeField]
    private UI_Powerup_statDB powerStatDB;

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
