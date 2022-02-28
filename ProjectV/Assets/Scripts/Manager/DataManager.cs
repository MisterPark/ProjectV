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

    // 캐릭터들 정보와 현재 선택된 캐릭터 정보
    [ArrayElementTitle("name")]
    public PlayerCharacterNode[] playerCharacterData = new PlayerCharacterNode[(int)PlayerCharacterName.END];
    [HideInInspector] public PlayerCharacterName currentPlayerCharacter = PlayerCharacterName.Character_01;

    // 상점에서 구입한 스탯들이 보관되는 곳
    public float[] powerUpStat = new float[(int)StatType.END];
    [SerializeField]
    private UI_Powerup_statDB powerStatDB;

    // SaveData 에서 가져온 데이터들
    public SaveData currentSaveData;

    // 현재 진행되고 있는 게임 데이터
    [SerializeField] public CurrentGameData currentGameData;

    [SerializeField] private float moneyInterest_Per;

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
        foreach (Powerup_DataType _powerUp in powerStatDB.Powerup_Type_List)
        {
            powerUpStat[(int)_powerUp.PowerType] = _powerUp.Powerup_Value * _powerUp.Rank;
        }
    }

    public void SetInitCurrentPowerupCount()
    {
        currentSaveData.currentPowerUpCount = 0;
    }

    public float m_moneyInterest_Per => moneyInterest_Per;

    public void BuyPowerup(Powerup_DataType data)
    {
        if (currentSaveData.currentGold > data.CurrentPowerupPrice)
        {
            int itemp = data.Rank + 1;
            data.SetRank(itemp);
            currentSaveData.currentPowerUpCount += 1;
            currentSaveData.currentGold -= data.CurrentPowerupPrice;
        }
    }

    public void PriceReset()
    {
        int tempvalue;
        int count = powerStatDB.GetCount();
        for (int repeat = 0; repeat < count; ++repeat)
        {
            tempvalue = powerStatDB.Powerup_Type_List[repeat].Powerup_Price;
            tempvalue = (tempvalue * (powerStatDB.Powerup_Type_List[repeat].Rank + 1)) + (int)((tempvalue * m_moneyInterest_Per * currentSaveData.currentPowerUpCount));
            powerStatDB.Powerup_Type_List[repeat].SetPrice(tempvalue);
        }
    }
}
