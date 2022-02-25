using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum CharacterName { Char1, Char2, Char3, END}
public class SaveDataManager : MonoBehaviour
{
    // ---싱글톤으로 선언--- 
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static SaveDataManager _instance; 
    public static SaveDataManager Instance 
    { 
        get 
        { 
            if (!_instance) 
            { 
                _container = new GameObject();
                _container.name = "DataController"; 
                _instance = _container.AddComponent(typeof(SaveDataManager)) as SaveDataManager; 
                DontDestroyOnLoad(_container); 
            } 
            return _instance; 
        } 
    } 
    
    // --- 게임 데이터 파일이름 설정 ---
    public string GameDataFileName = "StarfishData.json"; 
    
    // "원하는 이름(영문).json"
    public SaveData _saveData; 
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
        set
        {

        }
    } 
    
    private void Start() 
    {
        LoadGameData(); 
        SaveGameData();
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
        dataManager.currentSaveData = saveData;
    } 
    
    // 게임 저장하기
    public void SaveGameData() 
    {
        saveData = DataManager.Instance.currentSaveData;

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