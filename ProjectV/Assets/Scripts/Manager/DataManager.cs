using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CurrentGameData
{
    public PlayerCharacterName characterName;
    public int killCount = 0;
    public float gold = 0;
    public float totalPlayTime;
    public int playerLevel = 1;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    
#if UNITY_EDITOR
    //[ArrayElementTitle("name")]
#endif
    public PlayerCharacterData[] playerCharacterData = new PlayerCharacterData[(int)PlayerCharacterName.Knight];

    
    public float[] powerUpStat = new float[(int)StatType.END];
    [SerializeField]
    public UI_Powerup_statDB powerStatDB;

    
#if UNITY_EDITOR
    [ArrayElementTitle("kind")]
#endif
    public SkillDataElement[] skillDatas = new SkillDataElement[(int)SkillKind.End];

    public SaveData currentSaveData;

    [SerializeField] public CurrentGameData currentGameData;

    [SerializeField] private float moneyInterest_Per;


    public StageData[] stageDatas = new StageData[(int)StageKind.End];

    public Settings Settings;

    public Localization Localization;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SaveDataManager.Instance = GetComponent<SaveDataManager>();
        //UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void Setting_PowerStat()
    {
        foreach (Powerup_DataType _powerUp in powerStatDB.Powerup_Type_List)
        {
            powerUpStat[(int)_powerUp.PowerType] = _powerUp.Powerup_Value * _powerUp.Rank;
        }
    }

    public float m_moneyInterest_Per => moneyInterest_Per;

    public void BuyPowerup(Powerup_DataType data)
    {
        if ((currentSaveData.currentGold > data.CurrentPowerupPrice)
            && (data.MaxRank > data.Rank))
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

    public void PowerupReset()
    {
        currentSaveData.currentPowerUpCount = 0;
        currentSaveData.currentGold = currentSaveData.totalGold;
        int count = powerStatDB.GetCount();
        for (int repeat = 0; repeat < count; ++repeat)
        {
            powerStatDB.Powerup_Type_List[repeat].SetRank(0);
        }
        PriceReset();
    }

    void InitializeSkillData()
    {
        int count = (int)SkillKind.End;
        for (int i = 0; i < count; i++)
        {
            skillDatas[i].kind = (SkillKind)i;
        }
    }

    public void Localized()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.isLoaded)
        {
            var all = currentScene.GetRootGameObjects();
            //Debug.Log(all.Length);
            for (int i = 0; i < all.Length; i++)
            {
                GameObject go = all[i];

                if (go != null)
                {
                    Text text = go.GetComponent<Text>();
                    if (text != null)
                    {
                        text.text = text.text.Localized();
                    }

                    Text[] texts = go.GetComponentsInChildren<Text>();
                    for (int j = 0; j < texts.Length; j++)
                    {
                        texts[j].text = texts[j].text.Localized();
                    }
                }
            }
        }
    }
    void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "TitleScene":
                {
                    SaveDataManager.Instance.LoadGameData();
                    break;
                }
            case "ResultScene":
                {
                    SaveDataManager.Instance.SaveGameData();
                    break;
                }
            default:
                break;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyUp(KeyCode.Delete))
        {
            SaveDataManager.Instance.SaveDataDelete();
        }
#endif
    }


    public void CurrentGameDataSave()
    {
        currentSaveData.totalKillCount += currentGameData.killCount;
        currentSaveData.totalGold += (int)currentGameData.gold;
        currentSaveData.currentGold += (int)currentGameData.gold;
        currentSaveData.totalPlayTime += (int)currentGameData.totalPlayTime;

        currentGameData.killCount = 0;
        currentGameData.gold = 0f;
        currentGameData.totalPlayTime = 0f;
        currentGameData.playerLevel = 1;
        currentGameData.characterName = PlayerCharacterName.Witch;
    }
}
