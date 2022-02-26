using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum CharacterName { Char1, Char2, Char3, END}
public class SaveDataManager : MonoBehaviour
{
    // ---�̱������� ����--- 
    //static GameObject _container;
    //static GameObject Container
    //{
    //    get
    //    {
    //        return _container;
    //    }
    //}
    //static SaveDataManager Instance; 
    public static SaveDataManager Instance;
    //{ 
    //    get 
    //    { 
    //        //if (!_instance) 
    //        //{ 
    //        //    _container = new GameObject();
    //        //    _container.name = "DataController"; 
    //        //    _instance = _container.AddComponent(typeof(SaveDataManager)) as SaveDataManager; 
    //        //    DontDestroyOnLoad(_container); 
    //        //} 
    //        return _instance; 
    //    } 
    //} 
    
    // --- ���� ������ �����̸� ���� ---
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


    // ����� ���� �ҷ�����
    public void LoadGameData() 
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        
        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        { 
            print("�ҷ����� ����"); 
            string FromJsonData = File.ReadAllText(filePath); 
            _saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
        }
        
        // ����� ������ ���ٸ�
        else 
        { 
            print("���ο� ���� ����");            
            _saveData = new SaveData();
        }

        //////
        DataManager dataManager = DataManager.Instance;
        dataManager.currentSaveData = _saveData;
    } 
    
    // ���� �����ϱ�
    public void SaveGameData() 
    {
        DataManager dataManager = DataManager.Instance;
        _saveData = dataManager.currentSaveData;
        _saveData.totalKillCount += dataManager.currentGameData.killCount;
        _saveData.totalGold += dataManager.currentGameData.gold;
        _saveData.currentGold += dataManager.currentGameData.gold;
        //
        string ToJsonData = JsonUtility.ToJson(saveData); 
        string filePath = Application.persistentDataPath + GameDataFileName; 

        // �̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);
        
        print("����Ϸ�"); 

        
    } 
    
    // ������ �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    { 
        SaveGameData(); 
    } 
} 