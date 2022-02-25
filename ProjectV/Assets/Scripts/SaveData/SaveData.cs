using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // 직렬화

public class SaveData 
{ 
    // 각 챕터의 잠금여부
    public bool[] unLockCharacter = new bool[(int)CharacterName.END];
    public bool[] unLockStage = new bool[3];

    public int totalGold;
    public int currentGold;
}