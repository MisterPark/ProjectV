using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum CharacterName { Char1, Char2, Char3, END}
public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    public string GameDataFileName = "StarfishData.json"; 
    

    // "원하는 이름(영문).json"
    [HideInInspector] public SaveData _saveData;  // DataManager.current랑 같은 포인터라 이걸 없애든지, 깊은복사 하던지 해야할듯
    public SaveData saveData 
    {
        get 
        { 
            // 게임이 시작되면 자동으로 실행되도록
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


    // 저장된 게임 불러오기
    public void LoadGameData() 
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        
        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        { 
            print("불러오기 성공"); 
            string FromJsonData = File.ReadAllText(filePath); 
            _saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
        }
        
        // 저장된 게임이 없다면
        else 
        { 
            print("새로운 파일 생성");            
            _saveData = new SaveData();
        }

        //////
        DataManager dataManager = DataManager.Instance;
        dataManager.currentSaveData = _saveData;
    } 
    
    // 게임 저장하기
    public void SaveGameData() 
    {
        DataManager dataManager = DataManager.Instance;
        _saveData = dataManager.currentSaveData;
        _saveData.totalKillCount += dataManager.currentGameData.killCount;
        _saveData.totalGold += dataManager.currentGameData.gold;
        _saveData.currentGold += dataManager.currentGameData.gold;
        _saveData.totalPlayTime += dataManager.currentGameData.playTime;
        //
        string ToJsonData = JsonUtility.ToJson(saveData); 
        string filePath = Application.persistentDataPath + GameDataFileName; 
        
        // 이미 저장된 파일이 있다면 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);
        
        print("저장완료"); 

        
    } 
    
    // 게임을 종료하면 자동저장되도록
    private void OnApplicationQuit()
    { 
        SaveGameData(); 
    } 
} 