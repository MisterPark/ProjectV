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
    // �� é���� ��ݿ���
    public bool[] unLockCharacter = new bool[(int)CharacterName.END];
    public bool[] unLockStage = new bool[3];

    // ����
    public int totalKillCount;

    // ��
    public int totalGold;
    public int currentGold;

    public int currentPowerUpCount;
    [SerializeField] public List<powerUpSave> powerUpSaves;
    public float totalPlayTime;
}