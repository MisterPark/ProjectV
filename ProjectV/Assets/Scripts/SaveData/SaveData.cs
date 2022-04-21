using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class powerUpSave
{ 
    public StatType powerType;
    public int Rank;
}

[Serializable] // ����ȭ
public class SaveData 
{ 

    public int totalKillCount;
    public int totalGold;
    public int currentGold;
    public float totalPlayTime;

    public int currentPowerUpCount;
    [SerializeField] public List<powerUpSave> powerUpSaves = new List<powerUpSave>();

    // ����
    public SettingsData settingsData = new SettingsData();

    // �� é���� ��ݿ���
    //public bool[] unLockCharacter = new bool[(int)PlayerCharacterName.END];
    //public bool[] unLockStage = new bool[(int)StageKind.End];
}