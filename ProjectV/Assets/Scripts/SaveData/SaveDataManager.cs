using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum CharacterName { Char1, Char2, Char3, END}
public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    public string GameDataFileName = "StarfishData.json"; 
    

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


    private void Start() 
    {
        //LoadGameData();
        //SaveGameData();

    }


    public void LoadGameData() 
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        
        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        { 
            print("Load Game Data"); 
            string FromJsonData = File.ReadAllText(filePath); 
            _saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
        }
        
        // ����� ������ ���ٸ�
        else 
        { 
            print("Create Save Data");            
            _saveData = new SaveData();
        }

        //////
        DataManager dataManager = DataManager.Instance;
        dataManager.currentSaveData = _saveData;
        foreach (powerUpSave saveData in dataManager.currentSaveData.powerUpSaves)
        {
            foreach (Powerup_DataType powerupData in dataManager.powerStatDB.Powerup_Type_List)
            {
                if (saveData.powerType == powerupData.PowerType)
                {
                    powerupData.SetRank(saveData.Rank);
                    break;
                }
            }
        }
    } 
    
    public void SaveGameData() 
    {
        DataManager dataManager = DataManager.Instance;
        _saveData = dataManager.currentSaveData;
        _saveData.totalKillCount += dataManager.currentGameData.killCount;
        _saveData.totalGold += (int)dataManager.currentGameData.gold;
        _saveData.currentGold += (int)dataManager.currentGameData.gold;
        _saveData.totalPlayTime += dataManager.currentGameData.totalPlayTime;
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
        string filePath = Application.persistentDataPath + GameDataFileName; 
        
        File.WriteAllText(filePath, ToJsonData);
        
        print("Save Game Data"); 

        
    } 
    
    private void OnApplicationQuit()
    { 
        SaveGameData(); 
    } 
} 