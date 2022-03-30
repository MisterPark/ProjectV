using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviourEx
{
    public static SaveDataManager Instance;
    public string GameDataFileName = "ProjectV.json"; 
    

    // "���ϴ� �̸�(����).json"
    [HideInInspector] public SaveData _saveData;  // DataManager.current�� ���� �����Ͷ� �̰� ���ֵ���, �������� �ϴ��� �ؾ��ҵ�
    public SaveData saveData 
    {
        get 
        { 
            // ������ ���۵Ǹ� �ڵ����� ����ǵ���
            if(_saveData == null)
            { 
                LoadGameData(); 
                SaveGameData();
            }
            return _saveData;
        }

    }


    protected override void Start() 
    {
        base.Start();
        //LoadGameData();
        //SaveGameData();

    }


    public void LoadGameData() 
    {
        string filePath = Path.Combine(Application.persistentDataPath, GameDataFileName);

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        { 
            //Debug.Log($"Load Game Data {filePath}"); 
            string FromJsonData = File.ReadAllText(filePath); 
            _saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
        }
        
        // ����� ������ ���ٸ�
        else 
        {
            //Debug.Log($"Create Save Data {filePath}");            
            _saveData = new SaveData();
        }

        //////
        DataManager dataManager = DataManager.Instance;
        //
        dataManager.currentSaveData = new SaveData();

        dataManager.Settings.BGMVolume = _saveData.BGMVolume;
        dataManager.Settings.SoundVolume = _saveData.SoundVolume;
        dataManager.Settings.VisibleDamageNumbers = _saveData.VisibleDamageNumbers;
        dataManager.Settings.Language = _saveData.Language;

        dataManager.currentSaveData.totalKillCount = _saveData.totalKillCount;
        dataManager.currentSaveData.totalGold = _saveData.totalGold;
        dataManager.currentSaveData.currentGold = _saveData.currentGold;
        dataManager.currentSaveData.totalPlayTime = _saveData.totalPlayTime;

        dataManager.currentSaveData.currentPowerUpCount = _saveData.currentPowerUpCount;
        foreach (powerUpSave powerUp in _saveData.powerUpSaves)
        {
            foreach (Powerup_DataType powerupData in dataManager.powerStatDB.Powerup_Type_List)
            {
                if (powerUp.powerType == powerupData.PowerType)
                {
                    powerupData.SetRank(powerUp.Rank);
                    dataManager.currentSaveData.powerUpSaves.Add(powerUp);
                    break;
                }
            }
        }

    } 
    
    public void SaveGameData() 
    {
        DataManager dataManager = DataManager.Instance;
        SaveData currentSaveData = dataManager.currentSaveData;
        dataManager.CurrentGameDataSave();
        //_saveData = dataManager.currentSaveData;
        _saveData.totalKillCount = currentSaveData.totalKillCount;
        _saveData.totalGold = currentSaveData.totalGold;
        _saveData.currentGold = currentSaveData.currentGold;
        _saveData.totalPlayTime = currentSaveData.totalPlayTime;
        //설정
        _saveData.BGMVolume = dataManager.Settings.BGMVolume;
        _saveData.SoundVolume = dataManager.Settings.SoundVolume;
        _saveData.VisibleDamageNumbers = dataManager.Settings.VisibleDamageNumbers;
        _saveData.Language = dataManager.Settings.Language;
        //파워업
        _saveData.powerUpSaves.Clear();
        foreach (Powerup_DataType data in dataManager.powerStatDB.Powerup_Type_List)
        {
            powerUpSave _powerUpSave = new powerUpSave();
            _powerUpSave.powerType = data.PowerType;
            _powerUpSave.Rank = data.Rank;
            _saveData.powerUpSaves.Add(_powerUpSave);
        }
        //
        string ToJsonData = JsonUtility.ToJson(saveData); 
        string filePath = Path.Combine(Application.persistentDataPath, GameDataFileName);


        File.WriteAllText(filePath, ToJsonData);
        
        //Debug.Log($"Save Game Data {filePath}"); 

        
    } 
    
    private void OnApplicationQuit()
    {
        SaveGameData();
        //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Stage"))
        //{
        //    SaveGameData();
        //}
        //else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TitleScene")
        //{
        //    SaveGameData();
        //}
    }

    public void SaveDataDelete()
    {
        DataManager dataManager = DataManager.Instance;
        SaveData temp = dataManager.currentSaveData;
        _saveData.totalKillCount = 0;
        _saveData.totalGold = 0;
        _saveData.currentGold = 0;
        _saveData.totalPlayTime = 0f;
        _saveData.powerUpSaves.Clear();
        //설정
        _saveData.BGMVolume = dataManager.Settings.BGMVolume;
        _saveData.SoundVolume = dataManager.Settings.SoundVolume;
        _saveData.VisibleDamageNumbers = dataManager.Settings.VisibleDamageNumbers;
        _saveData.Language = dataManager.Settings.Language;
        foreach (Powerup_DataType data in dataManager.powerStatDB.Powerup_Type_List)
        {
            powerUpSave _powerUpSave = new powerUpSave();
            _powerUpSave.powerType = data.PowerType;
            _powerUpSave.Rank = 0;
            _saveData.powerUpSaves.Add(_powerUpSave);
        }
        //
        string ToJsonData = JsonUtility.ToJson(saveData);
        string filePath = Path.Combine(Application.persistentDataPath, GameDataFileName);

        File.WriteAllText(filePath, ToJsonData);
        //
        LoadGameData();
        UI_Powerup.instacne.OnClickResetButton();
    }
} 