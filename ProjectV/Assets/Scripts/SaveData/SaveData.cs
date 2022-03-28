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

    public int totalKillCount;
    public int totalGold;
    public int currentGold;
    public float totalPlayTime;

    public int currentPowerUpCount;
    [SerializeField] public List<powerUpSave> powerUpSaves = new List<powerUpSave>();

    // 설정
    public float BGMVolume = 1f;
    public float SoundVolume = 1f;
    public bool VisibleDamageNumbers = true;
    public Language Language;

    // 각 챕터의 잠금여부
    //public bool[] unLockCharacter = new bool[(int)PlayerCharacterName.END];
    //public bool[] unLockStage = new bool[(int)StageKind.End];
}