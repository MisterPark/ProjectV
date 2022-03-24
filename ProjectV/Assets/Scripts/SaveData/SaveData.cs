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

[Serializable] // 직렬화
public class SaveData 
{ 
    // 각 챕터의 잠금여부
    public bool[] unLockCharacter = new bool[(int)CharacterName.END];
    public bool[] unLockStage = new bool[3];

    // 몬스터
    public int totalKillCount;

    // 돈
    public int totalGold;
    public int currentGold;

    public int currentPowerUpCount;
    [SerializeField] public List<powerUpSave> powerUpSaves;
    public float totalPlayTime;
}